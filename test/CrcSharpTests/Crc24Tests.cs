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
    public class Crc24Tests
    {
        private byte[] _data;

        [SetUp]
        protected void SetUp()
        {
            _data = System.Text.ASCIIEncoding.ASCII.GetBytes("123456789");
        }

        [Test]
        public void Crc24_OpenPGP_Calculate()
        {
            var crc24 = new Crc(new CrcParameters(24, 0x864cfb, 0xb704ce, 0x000000, false, false));
            Assert.AreEqual(0x21cf02, crc24.CalculateAsNumeric(_data));
            Assert.IsTrue(crc24.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x02, 0xcf, 0x21 }));
        }

        [Test]
        public void Crc24_BLE_Calculate()
        {
            var crc24 = new Crc(new CrcParameters(24, 0x00065b, 0x555555, 0x000000, true, true));
            Assert.AreEqual(0xc25a56, crc24.CalculateAsNumeric(_data));
            Assert.IsTrue(crc24.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x56, 0x5a, 0xc2 }));
        }

        [Test]
        public void Crc24_FlexRay_A_Calculate()
        {
            var crc24 = new Crc(new CrcParameters(24, 0x5d6dcb, 0xfedcba, 0x000000, false, false));
            Assert.AreEqual(0x7979bd, crc24.CalculateAsNumeric(_data));
            Assert.IsTrue(crc24.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xbd, 0x79, 0x79 }));
        }

        [Test]
        public void Crc24_FlexRay_B_Calculate()
        {
            var crc24 = new Crc(new CrcParameters(24, 0x5d6dcb, 0xabcdef, 0x000000, false, false));
            Assert.AreEqual(0x1f23b8, crc24.CalculateAsNumeric(_data));
            Assert.IsTrue(crc24.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xb8, 0x23, 0x1f }));
        }

        [Test]
        public void Crc24_Interlaken_Calculate()
        {
            var crc24 = new Crc(new CrcParameters(24, 0x328b63, 0xffffff, 0xffffff, false, false));
            Assert.AreEqual(0xb4f3e6, crc24.CalculateAsNumeric(_data));
            Assert.IsTrue(crc24.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xe6, 0xf3, 0xb4 }));
        }

        [Test]
        public void Crc24_LTE_A_Calculate()
        {
            var crc24 = new Crc(new CrcParameters(24, 0x864cfb, 0x000000, 0x000000, false, false));
            Assert.AreEqual(0xcde703, crc24.CalculateAsNumeric(_data));
            Assert.IsTrue(crc24.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x03, 0xe7, 0xcd }));
        }

        [Test]
        public void Crc24_LTE_B_Calculate()
        {
            var crc24 = new Crc(new CrcParameters(24, 0x800063, 0x000000, 0x000000, false, false));
            Assert.AreEqual(0x23ef52, crc24.CalculateAsNumeric(_data));
            Assert.IsTrue(crc24.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x52, 0xef, 0x23 }));
        }
    }
}

