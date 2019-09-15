using Ha_shemNetworksApi.Api.ServiceInterfaces;
using Ha_shemNetworksApiCommon.Entities;
using Ha_shemNetworksApiCommon.Helper;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ha_shemNetworksApi.Api.Implementation
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly ApiDbContext _dbContext;
        private readonly Ha_shemNetworksApiCommon.Helper.PasswordHasher _passwordHelper;

        public UserService(IOptions<AppSettings> appSettings, ApiDbContext dbContext/*, Ha_shemNetworksApiCommon.Helper.PasswordHasher passwordHelper ApiDbContext.ApiDbContext dbContext, Ha_shemNetworksApiCommon.Helper.PasswordHasher passwordHelper*/)
        {
            _appSettings = appSettings.Value;
           _dbContext = dbContext;
            _passwordHelper = new Ha_shemNetworksApiCommon.Helper.PasswordHasher();
        }

        public User Authenticate(string username, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Username == username);

            // return null if user not found
            if (user == null)
                return null;
            else
            {
                if(_passwordHelper.VerifyHashedPassword(user.Password, password) != PasswordVerificationResult.Success)
                    return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            return _dbContext.Users.ToList().Select(x => {
                x.Password = null;
                return x;
            });
        }

        public User GetById(int id)
        {
            var user = _dbContext.Users.Where(d => d.Id == id).SingleOrDefault();

            // return user without password
            if (user != null)
                user.Password = null;

            return user;
        }

        public (bool,string,User) RegisterNewUser(User user)
        {
            try
            {
                if (!_dbContext.Users.Any(d => d.Username.Equals(user.Username)))
                {
                    user.Password = _passwordHelper.HashPassword(user.Password);
                    _dbContext.Users.Add(user);
                    _dbContext.SaveChanges();
                    user.Password = null;
                    return (true, "Registration was successful.", user);
                }

                return (false, "Username already exist.", user);
            }
            catch (Exception ex)
            {
                return (false, "Sorry, something went wrong", user);
            }
        }
    }
}
