﻿using System.Security.Cryptography;
using System.Text;
using Conduit.Features.User.Application.Interface;
using static Conduit.Features.User.Application.Authentication;

namespace Conduit.Infrastructure.security
{
    public class HashingService : IHashingService
    {
        private const int saltSize = 256 / 8;
        private const int keySize = 512 / 8;
        private const int iteration = 1000;
        private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

        public (byte[], byte[]) HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(saltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iteration, _hashAlgorithm, keySize);
            return (hash, salt);
        }

        public bool VerifyPassword(string inputPassword, UserAunthCredentials dbPassword)
        {
            var inputHash = Rfc2898DeriveBytes.Pbkdf2(inputPassword, dbPassword.passwordSalt, iteration, _hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(dbPassword.passwordHash, inputHash);
        }
    }
}
