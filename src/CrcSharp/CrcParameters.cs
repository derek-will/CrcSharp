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

namespace CrcSharp
{
    /// <summary>
    /// CRC algorithm parameters.
    /// </summary>
    public class CrcParameters
    {
        /// <summary>
        /// The width of the CRC algorithm in bits.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// The polynomial of the CRC algorithm.
        /// </summary>
        public ulong Polynomial { get; }

        /// <summary>
        /// The initial value used in the computation of the CRC check value.
        /// </summary>
        public ulong InitialValue { get; }

        /// <summary>
        /// The value which is XORed to the final computed value before returning the check value.
        /// </summary>
        public ulong XorOutValue { get; }

        /// <summary>
        /// Indicates whether bytes are reflected before being processed.
        /// </summary>
        public bool ReflectIn { get; }

        /// <summary>
        /// Indicates whether the final computed value is reflected before the XOR stage.
        /// </summary>
        public bool ReflectOut { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrcSharp.CrcParameters"/> class.
        /// </summary>
        /// <param name="width">Width of the CRC algorithm in bits.</param>
        /// <param name="polynomial">Polynomial of the CRC algorithm.</param>
        /// <param name="initialValue">Initial value used in the computation of the CRC check value.</param>
        /// <param name="xorOutValue">The value which is XORed to the final computed value before returning the check value.</param>
        /// <param name="reflectIn">If set to <c>true</c> each byte is to be reflected before being processed.</param>
        /// <param name="reflectOut">If set to <c>true</c> the final computed value is reflected before the XOR stage.</param>
        public CrcParameters(int width, ulong polynomial, ulong initialValue, ulong xorOutValue, bool reflectIn, bool reflectOut)
        {
            ThrowIfParametersInvalid(width, polynomial, initialValue, xorOutValue);

            Width = width;
            Polynomial = polynomial;
            InitialValue = initialValue;
            XorOutValue = xorOutValue;
            ReflectIn = reflectIn;
            ReflectOut = reflectOut;
        }

        private void ThrowIfParametersInvalid(int width, ulong polynomial, ulong initialValue, ulong xorOutValue)
        {
            if (width < 2 || width > 64)
                throw new ArgumentOutOfRangeException(nameof(width), "Width must be between 2-64 bits.");

            ulong maxValue = (UInt64.MaxValue >> (64 - width));

            if (polynomial > maxValue)
                throw new ArgumentOutOfRangeException(nameof(polynomial), $"Polynomial exceeds {width} bits.");

            if (initialValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(initialValue), $"Initial Value exceeds {width} bits.");

            if (xorOutValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(xorOutValue), $"XOR Out Value exceeds {width} bits.");
        }
    }
}