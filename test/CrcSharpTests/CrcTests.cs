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
using NUnit.Framework;
using CrcSharp;

namespace CrcSharpTests
{
	[TestFixture]
	public class CrcTests
	{
		[Test]
		public void Crc_Ctor_Valid()
		{
			var crc = new Crc(new CrcParameters(8, 0x07, 0x00, 0x00, false, false));
			Assert.IsNotNull(crc.Parameters);
			Assert.AreEqual(8, crc.Parameters.Width);
			Assert.AreEqual(0x07, crc.Parameters.Polynomial);
			Assert.AreEqual(0x00, crc.Parameters.InitialValue);
			Assert.AreEqual(0x00, crc.Parameters.XorOutValue);
			Assert.AreEqual(false, crc.Parameters.ReflectIn);
			Assert.AreEqual(false, crc.Parameters.ReflectOut);

			Assert.IsNotNull(crc.LookupTable);
			Assert.AreEqual(256, crc.LookupTable.Length);
		}

		[Test]
		public void Crc_Ctor_Null_CrcParameters()
		{
			Assert.Throws<ArgumentNullException>(() => new Crc(null));
		}

		[Test]
		public void Crc_CalculateCheckValue_Null_Data()
		{
			var crc = new Crc(new CrcParameters(8, 0x07, 0x00, 0x00, false, false));
			Assert.Throws<ArgumentNullException>(() => crc.CalculateCheckValue(null));
		}

		[Test]
		public void Crc_CalculateAsNumeric_Null_Data()
		{
			var crc = new Crc(new CrcParameters(8, 0x07, 0x00, 0x00, false, false));
			Assert.Throws<ArgumentNullException>(() => crc.CalculateAsNumeric(null));
		}
	}
}
