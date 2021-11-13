using System.Text;
using Microsoft.AspNetCore.Authentication;


namespace SecureToken.Authentication
{
    public class SecureTokenAuthenticationOptions : AuthenticationSchemeOptions
    {
        
        public SecureTokenOptions TokenOptions { get; set; }

        public string  AuthenticationHeader { get; set; }
        
    }
}
