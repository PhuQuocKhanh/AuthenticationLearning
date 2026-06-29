using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTAuthDemo.Data;
using JWTAuthDemo.Models;
using JWTAuthDemo.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JWTAuthDemo.Services
{
    public interface IUserService
    {
        Task<TokenResponse> Login(LoginRequest request);
    }
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly TokenService _tokenService;
        private readonly Jwt _jwt;

        public UserService(AppDbContext db, TokenService tokenService, IOptions<Jwt> jwt)
        {
            _db = db;
            _tokenService = tokenService;
            _jwt = jwt.Value;
        }

        public async Task<TokenResponse> Login(LoginRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Username == request.Username);
        
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user.Id);

            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();

            return new TokenResponse
            {
                AccessToken = accessToken,
                AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(15),
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiresAt = refreshToken.ExpiresAt
            };
        }

    }
}