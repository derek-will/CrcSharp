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
    public class Crc32Tests
    {
        private byte[] _data;

        [SetUp]
        protected void SetUp()
        {
            _data = System.Text.ASCIIEncoding.ASCII.GetBytes("123456789");
        }

        [Test]
        public void Crc32_ISO_HDLC_Calculate()
        {
            var crc32 = new Crc(new CrcParameters(32, 0x04c11db7, 0xffffffff, 0xffffffff, true, true));
            Assert.AreEqual(0xcbf43926, crc32.CalculateAsNumeric(_data));
            Assert.IsTrue(crc32.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x26, 0x39, 0xf4, 0xcb }));
        }

        [Test]
        public void Crc32_AUTOSAR_Calculate()
        {
            var crc32 = new Crc(new CrcParameters(32, 0xf4acfb13, 0xffffffff, 0xffffffff, true, true));
            Assert.AreEqual(0x1697d06a, crc32.CalculateAsNumeric(_data));
            Assert.IsTrue(crc32.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x6a, 0xd0, 0x97, 0x16 }));
        }

        [Test]
        public void Crc32_BZIP2_Calculate()
        {
            var crc32 = new Crc(new CrcParameters(32, 0x04c11db7, 0xffffffff, 0xffffffff, false, false));
            Assert.AreEqual(0xfc891918, crc32.CalculateAsNumeric(_data));
            Assert.IsTrue(crc32.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x18, 0x19, 0x89, 0xfc }));
        }

        [Test]
        public void Crc32_ISCSI_Calculate()
        {
            var crc32 = new Crc(new CrcParameters(32, 0x1edc6f41, 0xffffffff, 0xffffffff, true, true));
            Assert.AreEqual(0xe3069283, crc32.CalculateAsNumeric(_data));
            Assert.IsTrue(crc32.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x83, 0x92, 0x06, 0xe3 }));
        }

        [Test]
        public void Crc32_BASE91_D_Calculate()
        {
            var crc32 = new Crc(new CrcParameters(32, 0xa833982b, 0xffffffff, 0xffffffff, true, true));
            Assert.AreEqual(0x87315576, crc32.CalculateAsNumeric(_data));
            Assert.IsTrue(crc32.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x76, 0x55, 0x31, 0x87 }));
        }

        [Test]
        public void Crc32_MPEG2_Calculate()
        {
            var crc32 = new Crc(new CrcParameters(32, 0x04c11db7, 0xffffffff, 0x00000000, false, false));
            Assert.AreEqual(0x0376e6e7, crc32.CalculateAsNumeric(_data));
            Assert.IsTrue(crc32.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xe7, 0xe6, 0x76, 0x03 }));
        }

        [Test]
        public void Crc32_CKSUM_Calculate()
        {
            var crc32 = new Crc(new CrcParameters(32, 0x04c11db7, 0x00000000, 0xffffffff, false, false));
            Assert.AreEqual(0x765e7680, crc32.CalculateAsNumeric(_data));
            Assert.IsTrue(crc32.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x80, 0x76, 0x5e, 0x76 }));
        }

        [Test]
        public void Crc32_AIXM_Calculate()
        {
            var crc32 = new Crc(new CrcParameters(32, 0x814141ab, 0x00000000, 0x00000000, false, false));
            Assert.AreEqual(0x3010bf7f, crc32.CalculateAsNumeric(_data));
            Assert.IsTrue(crc32.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x7f, 0xbf, 0x10, 0x30 }));
        }

        [Test]
        public void Crc32_JAMCRC_Calculate()
        {
            var crc32 = new Crc(new CrcParameters(32, 0x04c11db7, 0xffffffff, 0x00000000, true, true));
            Assert.AreEqual(0x340bc6d9, crc32.CalculateAsNumeric(_data));
            Assert.IsTrue(crc32.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xd9, 0xc6, 0x0b, 0x34 }));
        }

        [Test]
        public void Crc32_XFER_Calculate()
        {
            var crc32 = new Crc(new CrcParameters(32, 0x000000af, 0x00000000, 0x00000000, false, false));
            Assert.AreEqual(0xbd0be338, crc32.CalculateAsNumeric(_data));
            Assert.IsTrue(crc32.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x38, 0xe3, 0x0b, 0xbd }));
        }
    }
}
