using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SecureToken.Authentication
{
    public class SecureTokenAuthenticationHandler : AuthenticationHandler<SecureTokenAuthenticationOptions>
    {
        public SecureTokenAuthenticationHandler(IOptionsMonitor<SecureTokenAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder urlEnc, ISystemClock clock)
            : base(options, logger, urlEnc, clock)
        {

        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string header = String.IsNullOrEmpty(Options.AuthenticationHeader) ?
                                       "Authorization" : Options.AuthenticationHeader;

            if (!Request.Headers.ContainsKey(header))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var token = Request.Headers[header].ToString();
            var cert = Token.CreateCertificate(token, Options.TokenOptions);
            if (cert == null || !cert.IsValid)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid token"));
            }

            var claims = cert.Claims
                .Select(claim => new Claim(claim.Key, claim.Value))
                .ToArray()
                .Append(new Claim(SecureTokenDefaults.ClaimTypes.Identifier, cert.Identifier))
                .Append(new Claim(SecureTokenDefaults.ClaimTypes.Issuer, cert.Issuer))
                .Append(new Claim(SecureTokenDefaults.ClaimTypes.ValidFrom, cert.ValidFrom.ToLongDateString()))
                .Append(new Claim(SecureTokenDefaults.ClaimTypes.ExpiresAt, cert.ExpiresAt.ToLongDateString()));

            var claimIdentity = new ClaimsIdentity(claims, SecureTokenDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimIdentity);
            var ticket = new AuthenticationTicket(principal, SecureTokenDefaults.AuthenticationScheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Headers["WWW-Authenticate"] =
                             $"{SecureTokenDefaults.AuthenticationScheme}, charset=\"UTF-8\"";

            return base.HandleChallengeAsync(properties);
        }
    }
}
