using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MachineKeyGenerator.Web
{
    public class KeyGenerator
    {
        public string GenerateKey(int length, bool useUpperCase = true)
        {
            byte[] buffer = new byte[length];
            var randomNumberGenerator = new RNGCryptoServiceProvider();
            randomNumberGenerator.GetBytes(buffer);

            return ToHexString(buffer, useUpperCase);
        }

        private static string ToHexString(byte[] bytes, bool useUpperCase = false)
        {
            var hex = string.Concat(bytes.Select(b => b.ToString(useUpperCase ? "X2" : "x2")));

            return hex;
        }
    }
}
