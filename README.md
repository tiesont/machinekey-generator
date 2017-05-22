# MKG - MachineKey Generator

This tool allows you to generate random keys for validation and encryption/decryption of the ViewState in your ASP.NET application.

The `<machineKey>` element is also used by the default Membership provider to hash/encrypt passwords, and is required when deploying your application to a web farm.

The tool creates a 256-bit decryption key and a 512-bit validation key, with Rijndael (specifically, AES) as the data validation algorithm. Once the keys are generated, they are converted into a string of hexadecimal characters.

### Tests needed

Internally, MKG uses the RNGCryptoServiceProvider class to generate the bytes from which the keys are derived. While the RNGCryptoServiceProvider is *supposed* to be sufficiently random to make it useful for crypto hashing, I have not yet written an exhaustive test suite to verify this. As such, I'd appreciate any pull-requests with units tests, if one is so inclined.

Currently, there is [one test project](https://github.com/tiesont/machinekey-generator/blob/master/MachineKeyGenerator/MachineKeyGenerator.Tests/KeyGenerator_Tests.cs) that runs a few simple tests. I use a HashSet to store 1 million generated keys, escaping early if a duplicate is detected. At the moment, the tests complete successfully, so I feel somewhat confident that you will always get a unique machine key set.

