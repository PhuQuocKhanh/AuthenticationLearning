using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BasicAuthDemo.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace BasicAuthDemo.Authentication
{
    public class BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        UserService userService) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, clock)
    {
        private UserService _userService = userService;

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]!);

            if (authHeader.Scheme != "Basic")
            {
                return AuthenticateResult.Fail("Invalid Authentication Scheme");
            }

            var credentialBytes = Convert.FromBase64String(authHeader.Parameter!);

            var credential = Encoding.UTF8.GetString(credentialBytes);

            var parts = credential.Split(':');

            var username = parts[0];
            var password = parts[1];

            if (!_userService.Validate(username, password))
            {
                return AuthenticateResult.Fail("Invalid username/password");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
            };

            var identity = new ClaimsIdentity(claims, "Basic");

            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, "Basic");

            return AuthenticateResult.Success(ticket);
        }
    }
}