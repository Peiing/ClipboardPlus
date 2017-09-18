using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace HashHelper
{
    class StringHashHelper
    {
        private static Dictionary<string, HashAlgorithm> algorithms = new Dictionary<string, HashAlgorithm>
        {
            { "md5", MD5.Create() },
            { "sha1", SHA1.Create() },
            { "sha256", SHA256.Create() },
            { "sha384", SHA384.Create() },
            { "sha512", SHA512.Create() },
            { "ripemd160", RIPEMD160.Create() }
        };

        public static string GetHash(string inputString, string type = "md5", bool toUpper = true)
        {
            if (!algorithms.ContainsKey(type))
            {
                type = "md5";
            }
            HashAlgorithm hasher = algorithms[type];
            var data = Encoding.UTF8.GetBytes(inputString);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hasher.ComputeHash(data))
            {
                sb.Append(b.ToString("X2"));
            }
            return toUpper ? sb.ToString() : sb.ToString().ToLower();
        }
    }
}
