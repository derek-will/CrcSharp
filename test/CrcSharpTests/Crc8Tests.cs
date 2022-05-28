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
    public class Crc8Tests
    {
        private byte[] _data;

        [SetUp]
        protected void SetUp()
        {
            _data = System.Text.ASCIIEncoding.ASCII.GetBytes("123456789");
        }

        [Test]
        public void Crc8_SMBus_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x07, 0x00, 0x00, false, false));
            Assert.AreEqual(0xf4, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xf4 }));
        }

        [Test]
        public void Crc8_AUTOSAR_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x2f, 0xff, 0xff, false, false));
            Assert.AreEqual(0xdf, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xdf }));
        }

        [Test]
        public void Crc8_CDMA2000_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x9b, 0xff, 0x00, false, false));
            Assert.AreEqual(0xda, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xda }));
        }

        [Test]
        public void Crc8_DARC_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x39, 0x00, 0x00, true, true));
            Assert.AreEqual(0x15, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x15 }));
        }

        [Test]
        public void Crc8_DVB_S2_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0xd5, 0x00, 0x00, false, false));
            Assert.AreEqual(0xbc, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xbc }));
        }

        [Test]
        public void Crc8_EBU_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x1d, 0xff, 0x00, true, true));
            Assert.AreEqual(0x97, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x97 }));
        }

        [Test]
        public void Crc8_I_CODE_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x1d, 0xfd, 0x00, false, false));
            Assert.AreEqual(0x7e, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x7e }));
        }

        [Test]
        public void Crc8_ITU_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x07, 0x00, 0x55, false, false));
            Assert.AreEqual(0xa1, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xa1 }));
        }

        [Test]
        public void Crc8_LTE_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x9b, 0x00, 0x00, false, false));
            Assert.AreEqual(0xea, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xea }));
        }

        [Test]
        public void Crc8_MAXIM_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x31, 0x00, 0x00, true, true));
            Assert.AreEqual(0xa1, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xa1 }));
        }

        [Test]
        public void Crc8_OPENSAFETY_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x2f, 0x00, 0x00, false, false));
            Assert.AreEqual(0x3e, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x3e }));
        }

        [Test]
        public void Crc8_ROHC_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x07, 0xff, 0x00, true, true));
            Assert.AreEqual(0xd0, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xd0 }));
        }

        [Test]
        public void Crc8_SAE_J1850_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x1d, 0xff, 0xff, false, false));
            Assert.AreEqual(0x4b, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x4b }));
        }

        [Test]
        public void Crc8_WCDMA_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x9b, 0x00, 0x00, true, true));
            Assert.AreEqual(0x25, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x25 }));
        }

        [Test]
        public void Crc8_GSM_A_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x1d, 0x00, 0x00, false, false));
            Assert.AreEqual(0x37, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x37 }));
        }

        [Test]
        public void Crc8_GSM_B_Calculate()
        {
            var crc8 = new Crc(new CrcParameters(8, 0x49, 0x00, 0xff, false, false));
            Assert.AreEqual(0x94, crc8.CalculateAsNumeric(_data));
            Assert.IsTrue(crc8.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x94 }));
        }
    }
}

