using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SecureToken.Authorization
{
    public static class AuthorizationToken
    {

        public static string Issue(string userName,
            string identifier, string issuer, IEnumerable<KeyValuePair<string, string>> claims,
            DateTime validFrom, TimeSpan duration, SecureTokenOptions options)
        {

            var newClaims =
                claims
                .Where(c => c.Key != ClaimTypes.Name)
                .Append(new KeyValuePair<string, string>(ClaimTypes.Name, userName))
                .ToArray();

            var cert = new Certificate(identifier, issuer, newClaims, validFrom, duration);
            return Token.CreateToken(cert, options);
        }

        public static string Issue(string userName, DateTime validFrom, TimeSpan duration, SecureTokenOptions options)
        {
            return Issue(userName, "", "", new List<KeyValuePair<string, string>>(), validFrom, duration, options);
        }

        public static string Issue(string userName, IEnumerable<KeyValuePair<string, string>> claims, DateTime validFrom, TimeSpan duration, SecureTokenOptions options)
        {
            return Issue(userName, "", "", claims, validFrom, duration, options);
        }

    }
}
