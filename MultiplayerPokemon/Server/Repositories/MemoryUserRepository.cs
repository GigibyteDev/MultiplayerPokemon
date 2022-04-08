﻿using Microsoft.IdentityModel.Tokens;
using MultiplayerPokemon.Server.Models;
using MultiplayerPokemon.Server.Repositories.Interfaces;
using MultiplayerPokemon.Server.Services.Interfaces;
using MultiplayerPokemon.Shared.Dtos;

namespace MultiplayerPokemon.Server.Repositories
{
    public class MemoryUserRepository : IUserRepository
    {
        private readonly ITokenService tokenService;
        private readonly List<User> users;
        private int userId = 1;
        public MemoryUserRepository(ITokenService _tokenService)
        {
            users = new List<User>
            {
                new User(0, new RegisterRequest
                {
                    Email = "Admin@MultiplayerPokemon.com",
                    Username = "Admin",
                    Password = "AdminPass123"
                }),
                new User(1, new RegisterRequest
                {
                    Email = "Admin2@MultiplayerPokemon.com",
                    Username = "Admin2",
                    Password = "AdminPass321"
                })
            };
            tokenService = _tokenService;
        }
        public SecurityToken? AuthorizeToken(string token)
        {
            return tokenService.AuthorizeToken(token);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return users.FirstOrDefault(u => u.Username.ToLower().Trim() == username.ToLower().Trim());
        }

        public async Task<LoginResult> Login(LoginRequest request)
        {
            User user;
            if (users.Any(u => u.Username.ToLower().Trim() == request.Username.ToLower().Trim()))
            {
                 user = users.Single(u => u.Username.ToLower().Trim() == request.Username.ToLower().Trim());
            }
            else
            {
                return new LoginResult{ ErrorMessage = "User Does Not Exist"};
            }

            bool validPassword = user.PasswordMatch(request.Password);

            if (!validPassword)
            {
                return new LoginResult { ErrorMessage = "Incorrect Password" };
            }

            string jwt = tokenService.CreateToken(user);

            if (!string.IsNullOrEmpty(jwt))
            {
                return new LoginResult { Success = true, JWT = jwt };
            }
            else
            {
                return new LoginResult { ErrorMessage = "Couldn't Generate Token" };
            }
        }

        public async Task<RegisterResult> Register(RegisterRequest request)
        {
            if (users.Any(u => u.Username.ToLower().Trim() == request.Username.ToLower().Trim()))
            {
                User user = users.Single(u => u.Username.ToLower().Trim() == request.Username.ToLower().Trim());
                return new RegisterResult { Id = user.Id };
            }

            users.Add(new User(userId++, request));

            return new RegisterResult { Id = userId };
        }
    }
}
