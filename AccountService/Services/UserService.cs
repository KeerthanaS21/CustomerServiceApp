using AccountService.Database;
using AccountService.Entities;
using AccountService.Interfaces;
using AccountService.Models;
using AccountService.Strings;
using AccountService.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccountService.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private TimeSpan ExpiryDuration = new TimeSpan(20, 30, 0);

        public UserService(UserManager<ApplicationUser> userManager, ILogger<UserService> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<BaseResponse<string>> RegisterUserAsync(RegisterAccount request)
        {
            var userEmailExist = await _userManager.FindByEmailAsync(request.UserEmail);

            if (userEmailExist != null)
            {
                return BaseResponse<string>.Error(AuthResponseStrings.AccountCannotbeCreated);
            }

            var userNameExist = await _userManager.FindByNameAsync(request.UserName);

            if (userNameExist != null)
            {
                return BaseResponse<string>.Error(AuthResponseStrings.AccountCannotbeCreated);
            }

            var user = new ApplicationUser
            {
                UserName = request.UserName,
                PasswordHash = request.UserPassword,
                Email = request.UserEmail,
                UserRole = request.UserRole
            };

            var resultUser = await _userManager.CreateAsync(user);
            //resultUser.CheckResult();

            return BaseResponse<string>.Ok(AuthResponseStrings.MessageAfterCreatingAccount);
        }

        public async Task<BaseResponse<AuthenticationModel>> LoginUserAsync(LoginCreds loginModel)
        {
            //var user = _userManager.Users.FirstOrDefault(x => x.Email == loginModel.UserEmail);
            var user = await _userManager.FindByEmailAsync(loginModel.UserEmail);
            if (user == null)
            {
                return BaseResponse<AuthenticationModel>.Error(
                    AuthResponseStrings.UserNotFoundMessage(loginModel.UserEmail));
            }

            bool passwordIsCorrect = !(_userManager.Users.Where(x => x.PasswordHash == loginModel.UserPassword).FirstOrDefault() is null) ? true : false; 
            if (!passwordIsCorrect)
            {
                return BaseResponse<AuthenticationModel>.Error(
                    AuthResponseStrings.IncorrectDataMessage(loginModel.UserEmail));
            }

            JwtSecurityToken token = GetToken(user);

            var modelResponse = new AuthenticationModel
            {
                UserName = user.UserName,
                UserRole = user.UserRole,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return BaseResponse<AuthenticationModel>.Ok(modelResponse);
        }

        private JwtSecurityToken GetToken(ApplicationUser user)
        {
            IEnumerable<string> audience = new[]
            {
                _configuration["Jwt:Audience"],
                _configuration["Jwt:ProfileAudience"]
            };

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("Role", user.UserRole.ToString())
            };

            claims.AddRange(audience.Select(aud => new Claim(JwtRegisteredClaimNames.Aud, aud)));

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], claims,
                expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
            return tokenDescriptor;
        }
    }
}
