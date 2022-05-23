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
using NUnit.Framework;
using CrcSharp;

namespace CrcSharpTests
{
	[TestFixture]
	public class Crc12Tests
	{
		private byte[] _data;

		[SetUp]
		protected void SetUp()
		{
			_data = System.Text.ASCIIEncoding.ASCII.GetBytes("123456789");
		}

		[Test]
		public void Crc12_CDMA2000_Calculate()
		{
			var crc12 = new Crc(new CrcParameters(12, 0xf13, 0xfff, 0x000, false, false));
			Assert.AreEqual(0xd4d, crc12.CalculateAsNumeric(_data));
			Assert.IsTrue(crc12.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x4d, 0x0d }));
		}

		[Test]
		public void Crc12_DECT_Calculate()
		{
			var crc12 = new Crc(new CrcParameters(12, 0x80f, 0x000, 0x000, false, false));
			Assert.AreEqual(0xf5b, crc12.CalculateAsNumeric(_data));
			Assert.IsTrue(crc12.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x5b, 0x0f }));
		}

		[Test]
		public void Crc12_UMTS_Calculate()
		{
			var crc12 = new Crc(new CrcParameters(12, 0x80f, 0x000, 0x000, false, true));
			Assert.AreEqual(0xdaf, crc12.CalculateAsNumeric(_data));
			Assert.IsTrue(crc12.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xaf, 0x0d }));
		}

		[Test]
		public void Crc12_GSM_Calculate()
		{
			var crc12 = new Crc(new CrcParameters(12, 0xd31, 0x000, 0xfff, false, false));
			Assert.AreEqual(0xb34, crc12.CalculateAsNumeric(_data));
			Assert.IsTrue(crc12.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x34, 0x0b }));
		}
	}
}
