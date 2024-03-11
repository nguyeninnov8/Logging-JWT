using Logging_JWT.Entities;
using Logging_JWT.Helpers;
using Logging_JWT.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Logging_JWT.Services
{
    /// <summary>
    /// Interface for user service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="model">The authentication request model.</param>
        /// <returns>Returns the authentication response.</returns>
        AuthenticateRespone Authenticate(AuthenticateRequest model);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>Returns a collection of users.</returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>Returns the user with the specified ID.</returns>
        User GetById(int userId);
    }

    /// <summary>
    /// Implementation of the user service.
    /// </summary>
    public class UserService : IUserService
    {
        private List<User> _user = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", UserName = "test", Password = "test" }
        };

        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <inheritdoc/>
        public AuthenticateRespone Authenticate(AuthenticateRequest model)
        {
            var user = _user.SingleOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

            if (user == null) return null;

            var token = GenerateToken(user);

            return new AuthenticateRespone(user, token);
        }

        /// <inheritdoc/>
        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public User GetById(int userId)
        {
            throw new NotImplementedException();
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
