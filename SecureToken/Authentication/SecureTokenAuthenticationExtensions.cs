using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace SecureToken.Authentication
{
    public static class SecureTokenAuthenticationExtensions
    {
        public static AuthenticationBuilder AddSecureTokenAuthentication(this AuthenticationBuilder builder,Action<SecureTokenOptions> config, string authenticationHeader = "Authorization")
        {
            var sOptions = new SecureTokenOptions { };
            config(sOptions);

            if (sOptions.Encryptor == null)
            {
                throw new Exception("SecureToken authentication requires an encryptor.");
            }

            if (sOptions.Signer == null)
            {
                throw new Exception("SecureToken authentication requires a signer.");
            }

            builder.Services.AddScoped
            (
                svc => new SecureTokenOptions
                {
                    Encryptor = sOptions.Encryptor,
                    Signer = sOptions.Signer
                }
            );

            return builder.AddScheme<SecureTokenAuthenticationOptions, SecureTokenAuthenticationHandler>
            (
                SecureTokenDefaults.AuthenticationScheme,
                opt =>
                {
                    opt.TokenOptions = sOptions;
                    opt.AuthenticationHeader = authenticationHeader;
                }
            );

        }
    }
}
