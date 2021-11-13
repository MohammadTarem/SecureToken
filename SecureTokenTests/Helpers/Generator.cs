using System;
using System.Collections.Generic;
using SecureToken;

namespace SecureTokenTests.Helpers
{
    public static class Generator
    {
        readonly static string Identifier = "1";
        readonly static string Issuer = "Me!";
        readonly static DateTime ValidFrom = DateTime.Now;
        readonly static DateTime ExpiresAt = DateTime.Now.AddSeconds(10);
        private static readonly List< KeyValuePair<string, string>> Claims = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("Name", "James"),
            new KeyValuePair<string, string>("LastName", "May")
        };

        public static Certificate GetNewCertificate()
        {
            return new Certificate
            (
                Identifier, Issuer, Claims, ValidFrom, ExpiresAt

            );

        }

        public static Certificate GetNewCertificateWithDate(DateTime from, DateTime to)
        {
            return new Certificate
            (
                Identifier, Issuer, Claims, from, to

            );

        }

        public static SecureTokenOptions GetTokenOptions()
        {
            return new SecureTokenOptions
            {
                Encryptor = new TestEncryptor(),
                Signer = new TestSigner()
            };
        }

    }
}
