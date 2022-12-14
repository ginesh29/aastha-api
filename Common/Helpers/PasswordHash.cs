using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace AASTHA2.Common.Helpers
{
    public static class PasswordHash
    {
        public static string GenerateHash(string password)
        {
            byte[] salt = new byte[128 / 8];
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
