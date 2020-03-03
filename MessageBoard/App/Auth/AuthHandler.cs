using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace App.Auth
{
    public class AuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;
        
        public AuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock, 
            IUserService userService) : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out var header))
            {
                var credentialBytes = Convert.FromBase64String(header.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(":");
             
                var user = new User{Username = credentials[0], Password = credentials[1]};

                if (_userService.Authenticate(user))
                {
                    var claims = new[] {new Claim(ClaimTypes.Name, user.Username)};
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    
                    return AuthenticateResult.Success(ticket);
                }
            }
            return AuthenticateResult.Fail("Something went wrong with authorization.");
        }
    }
}