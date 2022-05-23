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
	public class CrcParametersTests
	{
		[Test]
		public void CrcParameters_Ctor_Valid_Success()
		{
			var crcParams = new CrcParameters(32, 0x14c108e0, 0xffff0000, 0xeeaa00b1, true, false);
			Assert.AreEqual(32, crcParams.Width);
			Assert.AreEqual(0x14c108e0, crcParams.Polynomial);
			Assert.AreEqual(0xffff0000, crcParams.InitialValue);
			Assert.AreEqual(0xeeaa00b1, crcParams.XorOutValue);
			Assert.AreEqual(true, crcParams.ReflectIn);
			Assert.AreEqual(false, crcParams.ReflectOut);
		}

		[Test]
		public void CrcParameters_Ctor_Invalid_TooBigPolynomial()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new CrcParameters(32, 0x1FFFFFFFF, 0xffff0000, 0xeeaa00b1, true, false));
		}

		[Test]
		public void CrcParameters_Ctor_Invalid_TooBigInitValue()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new CrcParameters(32, 0xFFFFFFFF, 0x1FFFFFFFF, 0xeeaa00b1, true, false));
		}

		[Test]
		public void CrcParameters_Ctor_Invalid_TooBigXorOutValue()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new CrcParameters(32, 0xFFFFFFFF, 0xFFFFFFFF, 0x1FFFFFFFF, true, false));
		}

		[Test]
		public void CrcParameters_Ctor_Invalid_WidthTooSmall()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new CrcParameters(7, 0x0F, 0x0F, 0x0F, false, false));
		}

		[Test]
		public void CrcParameters_Ctor_Invalid_WidthTooBig()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new CrcParameters(65, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, false, false));
		}
	}
}

