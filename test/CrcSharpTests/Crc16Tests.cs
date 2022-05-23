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
	public class Crc16Tests
	{
		private byte[] _data;

		[SetUp]
		protected void SetUp()
		{
			_data = System.Text.ASCIIEncoding.ASCII.GetBytes("123456789");
		}

		[Test]
		public void Crc16_ARC_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x8005, 0x0000, 0x0000, true, true));
			Assert.AreEqual(0xbb3d, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x3d, 0xbb }));
		}

		[Test]
		public void Crc16_AUG_CCITT_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x1021, 0x1d0f, 0x0000, false, false));
			Assert.AreEqual(0xe5cc, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xcc, 0xe5 }));
		}

		[Test]
		public void Crc16_BUYPASS_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x8005, 0x0000, 0x0000, false, false));
			Assert.AreEqual(0xfee8, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xe8, 0xfe }));
		}

		[Test]
		public void Crc16_CCITT_FALSE_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x1021, 0xffff, 0x0000, false, false));
			Assert.AreEqual(0x29b1, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xb1, 0x29 }));
		}

		[Test]
		public void Crc16_CDMA2000_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0xc867, 0xffff, 0x0000, false, false));
			Assert.AreEqual(0x4c06, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x06, 0x4c }));
		}

		[Test]
		public void Crc16_CMS_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x8005, 0xffff, 0x0000, false, false));
			Assert.AreEqual(0xaee7, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xe7, 0xae }));
		}

		[Test]
		public void Crc16_DDS_110_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x8005, 0x800d, 0x0000, false, false));
			Assert.AreEqual(0x9ecf, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xcf, 0x9e }));
		}

		[Test]
		public void Crc16_DECT_R_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x0589, 0x0000, 0x0001, false, false));
			Assert.AreEqual(0x007e, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x7e, 0x00 }));
		}

		[Test]
		public void Crc16_DECT_X_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x0589, 0x0000, 0x0000, false, false));
			Assert.AreEqual(0x007f, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x7f, 0x00 }));
		}

		[Test]
		public void Crc16_DNP_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x3d65, 0x0000, 0xffff, true, true));
			Assert.AreEqual(0xea82, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x82, 0xea }));
		}

		[Test]
		public void Crc16_EN_13757_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x3d65, 0x0000, 0xffff, false, false));
			Assert.AreEqual(0xc2b7, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xb7, 0xc2 }));
		}

		[Test]
		public void Crc16_GENIBUS_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x1021, 0xffff, 0xffff, false, false));
			Assert.AreEqual(0xd64e, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x4e, 0xd6 }));
		}

		[Test]
		public void Crc16_LJ1200_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x6f63, 0x0000, 0x0000, false, false));
			Assert.AreEqual(0xbdf4, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xf4, 0xbd }));
		}

		[Test]
		public void Crc16_MAXIM_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x8005, 0x0000, 0xffff, true, true));
			Assert.AreEqual(0x44c2, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xc2, 0x44 }));
		}

		[Test]
		public void Crc16_MCRF4XX_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x1021, 0xffff, 0x0000, true, true));
			Assert.AreEqual(0x6f91, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x91, 0x6f }));
		}

		[Test]
		public void Crc16_OPENSAFETY_A_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x5935, 0x0000, 0x0000, false, false));
			Assert.AreEqual(0x5d38, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x38, 0x5d }));
		}

		[Test]
		public void Crc16_OPENSAFETY_B_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x755b, 0x0000, 0x0000, false, false));
			Assert.AreEqual(0x20fe, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xfe, 0x20 }));
		}

		[Test]
		public void Crc16_PROFIBUS_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x1dcf, 0xffff, 0xffff, false, false));
			Assert.AreEqual(0xa819, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x19, 0xa8 }));
		}

		[Test]
		public void Crc16_RIELLO_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x1021, 0xb2aa, 0x0000, true, true));
			Assert.AreEqual(0x63d0, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xd0, 0x63 }));
		}

		[Test]
		public void Crc16_T10_DIF_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x8bb7, 0x0000, 0x0000, false, false));
			Assert.AreEqual(0xd0db, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xdb, 0xd0 }));
		}

		[Test]
		public void Crc16_TMS37157_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x1021, 0x89ec, 0x0000, true, true));
			Assert.AreEqual(0x26b1, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xb1, 0x26 }));
		}

		[Test]
		public void Crc16_TELEDISK_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0xa097, 0x0000, 0x0000, false, false));
			Assert.AreEqual(0x0fb3, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xb3, 0x0f }));
		}

		[Test]
		public void Crc16_A_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x1021, 0xc6c6, 0x0000, true, true));
			Assert.AreEqual(0xbf05, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x05, 0xbf }));
		}

		[Test]
		public void Crc16_USB_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x8005, 0xffff, 0xffff, true, true));
			Assert.AreEqual(0xb4c8, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xc8, 0xb4 }));
		}

		[Test]
		public void Crc16_KERMIT_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x1021, 0x0000, 0x0000, true, true));
			Assert.AreEqual(0x2189, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x89, 0x21 }));
		}

		[Test]
		public void Crc16_MODBUS_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x8005, 0xffff, 0x0000, true, true));
			Assert.AreEqual(0x4b37, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x37, 0x4b }));
		}

		[Test]
		public void Crc16_X25_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x1021, 0xffff, 0xffff, true, true));
			Assert.AreEqual(0x906e, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x6e, 0x90 }));
		}

		[Test]
		public void Crc16_XMODEM_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x1021, 0x0000, 0x0000, false, false));
			Assert.AreEqual(0x31c3, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xc3, 0x31 }));
		}

		[Test]
		public void Crc16_GSM_Calculate()
		{
			var crc16 = new Crc(new CrcParameters(16, 0x1021, 0x0000, 0xffff, false, false));
			Assert.AreEqual(0xce3c, crc16.CalculateAsNumeric(_data));
			Assert.IsTrue(crc16.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x3c, 0xce }));
		}
	}
}

