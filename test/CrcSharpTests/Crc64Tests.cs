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
    public class Crc64Tests
    {
        private byte[] _data;

        [SetUp]
        protected void SetUp()
        {
            _data = System.Text.ASCIIEncoding.ASCII.GetBytes("123456789");
        }

        [Test]
        public void Crc64_Standard_Calculate()
        {
            var crc64 = new Crc(new CrcParameters(64, 0x42f0e1eba9ea3693, 0x0000000000000000, 0x0000000000000000, false, false));
            Assert.AreEqual(0x6c40df5f0b497347, crc64.CalculateAsNumeric(_data));
            Assert.IsTrue(crc64.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x47, 0x73, 0x49, 0x0b, 0x5f, 0xdf, 0x40, 0x6c }));
        }

        [Test]
        public void Crc64_WE_Calculate()
        {
            var crc64 = new Crc(new CrcParameters(64, 0x42f0e1eba9ea3693, 0xffffffffffffffff, 0xffffffffffffffff, false, false));
            Assert.AreEqual(0x62ec59e3f1a4f00a, crc64.CalculateAsNumeric(_data));
            Assert.IsTrue(crc64.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x0a, 0xf0, 0xa4, 0xf1, 0xe3, 0x59, 0xec, 0x62 }));
        }

        [Test]
        public void Crc64_XZ_Calculate()
        {
            var crc64 = new Crc(new CrcParameters(64, 0x42f0e1eba9ea3693, 0xffffffffffffffff, 0xffffffffffffffff, true, true));
            Assert.AreEqual(0x995dc9bbdf1939fa, crc64.CalculateAsNumeric(_data));
            Assert.IsTrue(crc64.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0xfa, 0x39, 0x19, 0xdf, 0xbb, 0xc9, 0x5d, 0x99 }));
        }

        [Test]
        public void Crc64_GO_ISO_Calculate()
        {
            var crc64 = new Crc(new CrcParameters(64, 0x000000000000001b, 0xffffffffffffffff, 0xffffffffffffffff, true, true));
            Assert.AreEqual(0xb90956c775a41001, crc64.CalculateAsNumeric(_data));
            Assert.IsTrue(crc64.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x01, 0x10, 0xa4, 0x75, 0xc7, 0x56, 0x09, 0xb9 }));
        }
    }
}