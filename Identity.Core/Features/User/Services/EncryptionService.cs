using Identity.Core.Features.User.Services.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Core.Features.User.Services
{
    public class EncryptionService : IEncryptionService
    {
        public string CreateSaltKey(int size)
        {
            var rng = RandomNumberGenerator.Create();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string CreatePasswordHash(string password, string saltkey)
        {
            var cryptoServiceProvider = SHA1.Create();

            var saltAndPassword = string.Concat(password, saltkey);
            var hash = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return Encoding.UTF8.GetString(hash);
        }
    }
}
