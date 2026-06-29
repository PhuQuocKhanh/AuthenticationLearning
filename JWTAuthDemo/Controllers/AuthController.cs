using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTAuthDemo.Data;
using JWTAuthDemo.Models;
using JWTAuthDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthDemo.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;

        private readonly TokenService _tokenService;



        public AuthController(
            AppDbContext db,
            TokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
                LoginRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Username == request.Username);

            if (user == null)
            {
                return Unauthorized();
            }

            var accessToken =_tokenService.GenerateAccessToken(user);
            var refreshToken =_tokenService.GenerateRefreshToken(user.Id);
            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();

            return Ok(new TokenResponse
            {

                AccessToken = accessToken,

                AccessTokenExpiresAt =
                DateTime.UtcNow.AddMinutes(15),


                RefreshToken =
                refreshToken.Token,


                RefreshTokenExpiresAt =
                refreshToken.ExpiresAt

            });
        }
    }
}