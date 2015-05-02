#MKG - MachineKey Generator

This tool allows you to generate random keys for validation and encryption/decryption of the ViewState in your ASP.NET application.

The `<machinekey>` element is also used by the default Membership provider to hash/encrypt passwords, and is required when deploying your application to a web farm.

The tool creates a 256-bit decryption key and a 512-bit validation key, with Rijndael (specifically, AES) as the data validation algorithm. Once the keys are generated, they are converted into a string of hexadecimal characters.

