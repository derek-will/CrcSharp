#region License
/* 
BSD 3-Clause License

Copyright (c) 2017, Derek Will
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

3. Neither the name of the copyright holder nor the names of its
   contributors may be used to endorse or promote products derived from
   this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
*/
#endregion

using System;
using System.Linq;

namespace CrcSharp
{
	/// <summary>
	/// Computes the CRC check value for the input data.
	/// </summary>
	public class Crc
	{
		/// <summary>
		/// The CRC algorithm parameters.
		/// </summary>
		public CrcParameters Parameters { get; }

        /// <summary>
        /// The lookup table used in calculating check values.
        /// </summary>
        public ulong[] LookupTable { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrcSharp.Crc"/> class.
        /// </summary>
        public Crc(CrcParameters parameters)
		{
			Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters), "Parameters cannot be null.");
			LookupTable = GenerateLookupTable();
		}

        /// <summary>
        /// Calculates the CRC check value as a numeric value.
        /// </summary>
        /// <param name="data">Data to compute the check value for.</param>
        /// <returns>The CRC check value as a numeric value.</returns>
		public ulong CalculateAsNumeric(byte[] data)
		{
			byte[] crcCheckVal = CalculateCheckValue(data);
			Array.Resize(ref crcCheckVal, 8);
			return BitConverter.ToUInt64(crcCheckVal, 0);
		}

		/// <summary>
		/// Calculates the CRC check value as a byte array.
		/// </summary>
        /// <param name="data">Data to compute the check value for.</param>
		/// <returns>The CRC check value as a byte array.</returns>
		public byte[] CalculateCheckValue(byte[] data)
		{
			if (data == null)
				throw new ArgumentNullException(nameof(data), "Data argument cannot be null.");

			ulong crc = Parameters.InitialValue;

			if (Parameters.ReflectIn) 
			{
				crc = ReflectBits(crc, Parameters.Width);
			}

			foreach (byte b in data) 
			{
				if (Parameters.ReflectIn) 
				{
					crc = LookupTable[(crc ^ b) & 0xFF] ^ (crc >> 8);
				} 
				else 
				{
					crc = LookupTable[((crc >> (Parameters.Width - 8)) ^ b) & 0xFF] ^ (crc << 8);
				}

				crc &= (UInt64.MaxValue >> (64 - Parameters.Width));
			}

			// Source: https://stackoverflow.com/questions/28656471/how-to-configure-calculation-of-crc-table/28661073#28661073
			// Per Mark Adler - ...the reflect out different from the reflect in (CRC-12/3GPP). 
			// In that one case, you need to bit reverse the output since the input is not reflected, but the output is.
			if (Parameters.ReflectIn ^ Parameters.ReflectOut) 
			{
				crc = ReflectBits(crc, Parameters.Width);
			}

			ulong crcFinalValue = crc ^ Parameters.XorOutValue;
			return BitConverter.GetBytes(crcFinalValue).Take((Parameters.Width + 7)/ 8).ToArray();
		}

		private ulong[] GenerateLookupTable()
		{
			if (Parameters == null)
				throw new InvalidOperationException("CRC parameters must be set prior to calling this method.");

			var lookupTable = new ulong[256];
			ulong topBit = (ulong)((ulong)1 << (Parameters.Width - 1));

			for (int i = 0; i < lookupTable.Length; i++) 
			{
				byte inByte = (byte)i;
				if (Parameters.ReflectIn) 
				{
					inByte = (byte)ReflectBits(inByte, 8);
				}

				ulong r = (ulong)((ulong)inByte << (Parameters.Width - 8));
				for (int j = 0; j < 8; j++)
				{
					if ((r & topBit) != 0)
					{
						r = ((r << 1) ^ Parameters.Polynomial);
					}
					else
					{
						r = (r << 1);
					}
				}

				if (Parameters.ReflectIn)
				{
					r = ReflectBits(r, Parameters.Width);
				}

				lookupTable[i] = r & (UInt64.MaxValue >> (64 - Parameters.Width));
			}

			return lookupTable;
		}

		private static ulong ReflectBits(ulong b, int bitCount)
		{
			ulong reflection = 0x00;

			for (int bitNumber = 0; bitNumber < bitCount; ++bitNumber)
			{
				if (((b >> bitNumber) & 0x01) == 0x01)
				{
					reflection |= (ulong)(((ulong)1 << ((bitCount - 1) - bitNumber)));
				}
			}

			return reflection;
		}
	}
}