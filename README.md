[![NuGet](https://img.shields.io/nuget/v/CrcSharp.svg)](https://www.nuget.org/packages/CrcSharp)

## CRC# (CrcSharp)

This repository contains a .NET implementation for computing CRC (Cyclic Redundancy Check) values. This repo includes the library implementation and associated unit tests.

The library can be used to compute a CRC value using a `Crc` instance that is configured with a `CrcParameters` object. The `CrcParameters` class contains properties for width, polynomial, initial value, XOR out value, reflect in and reflect out settings. 
```cs
byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes("123456789"); // sample data
var crc32 = new Crc(new CrcParameters(32, 0x04c11db7, 0xffffffff, 0xffffffff, true, true)); // Standard CRC-32 configuration
ulong checkValue = crc32.CalculateAsNumeric(data); //0xcbf43926
byte[] checkValArray = crc32.CalculateCheckValue(data) // [0x26, 0x39, 0xf4, 0xcb]
```

### Credits:

This library would not have been possible had it not been for Ross Williams’ [A PAINLESS GUIDE TO CRC ERROR DETECTION ALGORITHMS](http://www.ross.net/crc/download/crc_v3.txt). I learned so much from the guide and the parameterization model defined within was a huge help. The CRC RevEng project’s [‘Catalogue of parametrised CRC algorithms’](http://reveng.sourceforge.net/crc-catalogue/) which is maintained by Greg Cook was the source of information for the unit tests.

### Additional Information:

* [License](LICENSE.md)
