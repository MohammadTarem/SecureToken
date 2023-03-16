
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthWithSecureToken.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginDto Login { get; set; }
        public void OnGet()
        {

        }

        public void OnPost()
        {

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "TestUser"));
            
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            
            HttpContext.SignInAsync(claimPrincipal).Wait();
        }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
