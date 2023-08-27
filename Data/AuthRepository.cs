using EventEnroll.Data;
using EventEnroll.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventEnroll.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private double EXPIRATION_MINUTES = 120;

        public AuthRepository(DataContext context, IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            // find the user in the database
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!await _userManager.CheckPasswordAsync(user, password))
            {
                response.Success = false;
                response.Message = "Incorrect password.";
            }
            else
            {
                response.Data = CreateToken(user);
            }
            return response;
        }

        public async Task<ServiceResponse<string>> Register(RegisterUserDto user)
        {
            var response = new ServiceResponse<string>();
            if (await UserExists(user.UserName))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }
            // creates a new IdentityUser and adds it to the database

            var result = await _userManager.CreateAsync(new IdentityUser() { UserName = user.UserName }, user.Password);

            if (!result.Succeeded)
            {
                response.Success = false;
                response.Message = "User creation failed! Please check user details and try again.";
                return response;
            }
            // gets the newly created Id from the database and asign to response.Data
            response.Data = (await _userManager.FindByNameAsync(user.UserName)).Id;
  
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            // checks the Db if the username already exists
            if(await _userManager.FindByNameAsync(username) != null)
            {
                return true;
            }
            return false;
        }
        private string CreateToken(IdentityUser user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken is null)
                throw new Exception("AppSettings:Token is missing from appsettings.json");

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettingsToken));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);


        }
    }
}
