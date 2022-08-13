[![NuGet](https://img.shields.io/nuget/v/CrcSharp.svg)](https://www.nuget.org/packages/CrcSharp)

## CRC# (CrcSharp)

This repository contains a .NET implementation for computing CRC (Cyclic Redundancy Check) values. This repo includes the library implementation and associated unit tests.

The library can be used to compute a CRC value using a `Crc` instance that is configured with a `CrcParameters` object. The `CrcParameters` class contains properties for width, polynomial, initial value, XOR out value, reflect in and reflect out settings. 
```cs
byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes("123456789"); // sample data
var crc32 = new Crc(new CrcParameters(32, 0x04c11db7, 0xffffffff, 0xffffffff, true, true)); // Standard CRC-32 configuration
ulong checkValue = crc32.CalculateAsNumeric(data); //0xcbf43926
byte[] checkValArray = crc32.CalculateCheckValue(data); // [0x26, 0x39, 0xf4, 0xcb]
```

### Officially Tested CRCs

| Family  | Count | Models             | 
|:--------|:------|:-------------------|
| CRC-3   | 2    | GSM, ROHC | 
| CRC-4   | 2    | G-704, Interlaken | 
| CRC-5   | 3    | EPC-C1G2, G-704, USB | 
| CRC-6   | 5    | CDMA2000-A, CDMA2000-B, DARC, G-704, GSM | 
| CRC-7   | 3    | MMC, ROHC, UMTS | 
| CRC-8   | 20    | AUTOSAR, CDMA2000, DARC, DVB-S2, EBU, I-CODE, ITU, GSM-A, GSM-B, ROHC, OpenSAFETY, J1850, LTE, Maxim, SMBus, WCDMA, BLUETOOTH, HITAG, MIFARE-MAD, NRSC-5 | 
| CRC-10  | 3     | ATM, CDMA2000, GSM |
| CRC-11  | 2     | FlexRay, UMTS |
| CRC-12  | 4     | CDMA2000, UMTS, DECT, GSM |
| CRC-13  | 1     | BBC |
| CRC-14  | 2     | GSM, DARC |
| CRC-15  | 2     | CAN, MPT1327 |
| CRC-16  | 31    | ARC, AUG-CCITT, UMTS, CCITT-FALSE, CDMA2000, CMS, DDS-110, DECT-R, DECT-X, DNP, EN-13757, GENIBUS, LJ1200, Maxim, MCRF4XX, OpenSAFETY-A, OpenSAFETY-B, PROFIBUS, Riello, T10-DIF, TMS37157, Teledisk, ISO-IEC-14443-3-A, USB, KERMIT, Modbus, IBM-SDLC, XMODEM, GSM, M17, NRSC-5 |
| CRC-17  | 1     | CAN-FD |
| CRC-21  | 1     | CAN-FD |
| CRC-24  | 8     | OpenPGP, BLE, FlexRay-A, FlexRay-B, Interlaken, LTE-A, LTE-B, OS-9 |
| CRC-30  | 1     | CDMA |
| CRC-31  | 1     | Philips |
| CRC-32  | 12    | ISO-HDLC, AUTOSAR, bzip2, ISCSI, BASE91-D, MPEG-2, cksum, AIXM, JAMCRC, XFER, CD-ROM-EDC, MEF |
| CRC-40  | 1     | GSM |
| CRC-64  | 5     | ECMA-182, Go ISO, WE, XZ, MS |
| Total   | 110   |  |

### Credits:

This library would not have been possible had it not been for Ross Williams’ [A PAINLESS GUIDE TO CRC ERROR DETECTION ALGORITHMS](http://www.ross.net/crc/download/crc_v3.txt). I learned so much from the guide and the parameterization model defined within was a huge help. The CRC RevEng project’s [‘Catalogue of parametrised CRC algorithms’](http://reveng.sourceforge.net/crc-catalogue/) which is maintained by Greg Cook was the source of information for the unit tests. In addition, the answers provided by Mark Adler on various Stack Overflow questions were a huge help in general, but especially on how to calculate CRCs that are under 8 bits in length and how to handle the scenario when ReflectIn != ReflectOut.

### Additional Information:

* [License](LICENSE.md)
