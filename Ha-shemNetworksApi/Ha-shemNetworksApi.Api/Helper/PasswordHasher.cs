using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Ha_shemNetworksApiCommon.Helper
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16; // 128 bit 
        private const int KeySize = 32; // 256 bit

        public PasswordHasher()
        {
            //Options = options.Value;
        }

        private HashingOptions Options { get; }

        public string HashPassword(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
     password,
     SaltSize,
     HashingOptions.Iterations,
     HashAlgorithmName.SHA256))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{HashingOptions.Iterations}.{salt}.{key}";
            }

        }

        public Microsoft.AspNet.Identity.PasswordVerificationResult VerifyHashedPassword(string hash, string password)
        {
            var parts = hash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. " +
                  "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            var needsUpgrade = iterations != HashingOptions.Iterations;

            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              salt,
              iterations,
              HashAlgorithmName.SHA512))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                var verified = keyToCheck.SequenceEqual(key);

                return verified ? Microsoft.AspNet.Identity.PasswordVerificationResult.Success : Microsoft.AspNet.Identity.PasswordVerificationResult.Failed;
            }
        }

    }
}
