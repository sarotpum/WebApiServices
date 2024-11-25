using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedService.Models;
using SharedService.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApiServices.Controllers.UserController
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly SignInManager<AppUserModel> _signInManager;
        private readonly JWTConfig _jwtConfig;

        public UserController(UserManager<AppUserModel> userManager, SignInManager<AppUserModel> signInManager, IOptions<JWTConfig> jwtConfig)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtConfig = jwtConfig.Value;
        }

        [HttpPost("RegisterUser")]
        public async Task<object> RegisterUser([FromBody] AddUpdateRegisterUserBindingModel model)
        {
            try
            {
                var user = new AppUserModel()
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    UserName = model.Email,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    return await Task.FromResult("User has been Register");

                return await Task.FromResult(result.Errors.Select(s => s.Description).ToArray());
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("GetAllUser")]
        public async Task<object> GetAllUsers()
        {
            try
            {
                var users = _userManager.Users.Select(s => new UserDTOModel(s.FullName, s.Email, s.UserName, s.DateCreated));
                return await Task.FromResult(users);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        var appUser = await _userManager.FindByEmailAsync(model.Email);
                        var user = new UserDTOModel(appUser.FullName, appUser.Email, appUser.UserName, appUser.DateCreated);
                        user.Token = GenerateToken(appUser);
                        return await Task.FromResult(user);
                        // return await Task.FromResult("login successfully");
                    }
                }
                // if (model.Email == "" || model.Password == "")
                // {
                //     return await Task.FromResult("Parameter are missing");
                // } 

                return await Task.FromResult("invalid Email or Password");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        private string GenerateToken(AppUserModel appUser)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]{
                new Claim(JwtRegisteredClaimNames.NameId, appUser.Id),
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtConfig.Audience,
                Issuer = _jwtConfig.Issuer
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
