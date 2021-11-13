using System;
using System.Collections.Generic;


namespace SecureToken
{
    public class Certificate
    {
        public DateTime ValidFrom { get; protected set; }
        public DateTime ExpiresAt { get; protected set; }


        public string Identifier { get; protected set; }
        public string Issuer { get; protected set;}
        
        public IEnumerable<KeyValuePair<string, string>> Claims  { get; set; }

        public Certificate(string identifier, string issuer,
            IEnumerable<KeyValuePair<string, string>> claims, DateTime validFrom, DateTime expiresAt)
        {
            Identifier = identifier;
            Issuer = issuer;
            ValidFrom = validFrom;
            ExpiresAt = expiresAt;
            Claims = claims ==  null ? new List<KeyValuePair<string, string>>() : claims;
        }

        public Certificate(string identifier, string issuer,
            IEnumerable<KeyValuePair<string, string>> claims, DateTime validFrom, TimeSpan duration)
                : this(identifier, issuer, claims, validFrom, validFrom.Add(duration)) { }

        public bool IsValid 
        {
            get
            {
                var now = DateTime.Now.Ticks;
                return now >= ValidFrom.Ticks && now <= ExpiresAt.Ticks;
            }
        }


    }
}
