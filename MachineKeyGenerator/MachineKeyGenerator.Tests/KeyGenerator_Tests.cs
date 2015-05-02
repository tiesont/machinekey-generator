using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

namespace MachineKeyGenerator.Tests
{
    [TestClass]
    public class KeyGenerator_Tests
    {
        int validationKeySize = 64;
        int decryptionKeySize = 32;


        [TestMethod]
        public void Generate_ValidationKey_Should_Not_Duplicate_Keys()
        {
            int iterations = 1000000;

            var generator = new MachineKeyGenerator.Web.KeyGenerator();
            HashSet<string> set = new HashSet<string>();

            for (int i = 0; i < iterations; i++)
            {
                if (!set.Add(generator.GenerateKey(validationKeySize)))
                {
                    break;
                }
            }

            set.Count.ShouldBeEquivalentTo<int>(iterations, "RNGCryptoServiceProvider is fairly random, and should generate the specified number of random keys without duplicates.");
        }

        [TestMethod]
        public void Generate_DecryptionKey_Should_Not_Duplicate_Keys()
        {
            int iterations = 1000000;

            var generator = new MachineKeyGenerator.Web.KeyGenerator();
            HashSet<string> set = new HashSet<string>();

            for (int i = 0; i < iterations; i++)
            {
                if (!set.Add(generator.GenerateKey(decryptionKeySize)))
                {
                    break;
                }
            }

            set.Count.ShouldBeEquivalentTo<int>(iterations, "RNGCryptoServiceProvider is fairly random, and should generate the specified number of random keys without duplicates.");
        }

        [TestMethod]
        public void Generate_BothKeys_Should_Not_Duplicate_Keys()
        {
            int iterations = 1000000;

            var generator = new MachineKeyGenerator.Web.KeyGenerator();
            HashSet<string> validationSet = new HashSet<string>();
            HashSet<string> decryptionSet = new HashSet<string>();

            for (int i = 0; i < iterations; i++)
            {
                if (!validationSet.Add(generator.GenerateKey(validationKeySize)))
                {
                    break;
                }

                if (!decryptionSet.Add(generator.GenerateKey(decryptionKeySize)))
                {
                    break;
                }
            }

            validationSet.Count.ShouldBeEquivalentTo<int>(iterations, "RNGCryptoServiceProvider is fairly random, and should generate the specified number of validation keys without duplicates.");

            decryptionSet.Count.ShouldBeEquivalentTo<int>(iterations, "RNGCryptoServiceProvider is fairly random, and should generate the specified number of decryption keys without duplicates.");
        }
    }
}
