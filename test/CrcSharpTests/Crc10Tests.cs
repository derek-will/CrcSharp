﻿#region License
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
    public class Crc10Tests
    {
        private byte[] _data;

        [SetUp]
        protected void SetUp()
        {
            _data = System.Text.ASCIIEncoding.ASCII.GetBytes("123456789");
        }

        [Test]
        public void Crc10_ATM_Calculate()
        {
            var crc10 = new Crc(new CrcParameters(10, 0x233, 0x000, 0x000, false, false));
            Assert.AreEqual(0x199, crc10.CalculateAsNumeric(_data));
            Assert.IsTrue(crc10.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x99, 0x01 }));
        }

        [Test]
        public void Crc10_CDMA2000_Calculate()
        {
            var crc10 = new Crc(new CrcParameters(10, 0x3d9, 0x3ff, 0x000, false, false));
            Assert.AreEqual(0x233, crc10.CalculateAsNumeric(_data));
            Assert.IsTrue(crc10.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x33, 0x02 }));
        }

        [Test]
        public void Crc10_GSM_Calculate()
        {
            var crc10 = new Crc(new CrcParameters(10, 0x175, 0x000, 0x3ff, false, false));
            Assert.AreEqual(0x12a, crc10.CalculateAsNumeric(_data));
            Assert.IsTrue(crc10.CalculateCheckValue(_data).SequenceEqual(new byte[] { 0x2a, 0x01 }));
        }
    }
}
