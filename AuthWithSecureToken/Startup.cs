﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using SecureToken.Authentication;
using SecureToken;

namespace AuthWithSecureToken
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {



            var rsa = new System.Security.Cryptography.RSACryptoServiceProvider(4096);

            services.AddControllers();
            services.AddRazorPages();
            //services.AddSecureTokenAuthentication(
            //config =>
            //   {
            //       config.Encryptor = new AesEncryptor(SecureRandomBytes.Generate(32),
            //                                           SecureRandomBytes.Generate(16));

            //       config.Signer = new PBKDF2Signer(SecureRandomBytes.Generate(64), 128, 10000);
            //   }
            //, "Token");

            services.AddSecureTokenAuthentication(config => 
            {
                config.Encryptor = new AesEncryptor(SecureRandomBytes.Generate(32),
                                                    SecureRandomBytes.Generate(16));

                config.Signer = new PBKDF2Signer(SecureRandomBytes.Generate(64), 128, 10000);
            }, CookieAuthenticationDefaults.AuthenticationScheme, "Token")
            .AddCookie(options => 
            { 
                options.LoginPath = "/login";
            });
            


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
