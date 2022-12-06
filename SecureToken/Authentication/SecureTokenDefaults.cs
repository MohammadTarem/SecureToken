namespace SecureToken.Authentication
{
    public static class SecureTokenDefaults
    {
        public static string AuthenticationScheme = "SecureToken";

        public static class ClaimTypes
        {
            public static string Issuer = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/issuer";
            public static string Identifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/identity";
            public static string ValidFrom = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/validfrom";
            public static string ExpiresAt = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/expiresat";
        }
    }
}
