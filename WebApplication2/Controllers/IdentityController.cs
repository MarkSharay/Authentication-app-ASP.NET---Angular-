using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApplication2.Data.Models;
using WebApplication2.Models.Identity;
using static Humanizer.In;
using WebApplication2.Services;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace WebApplication2.Controllers
{
    public class IdentityController : ApiController
    {
        private IOptions<AppSettings> appSettings;
        private readonly UserManager<User> userManager;
        //private HttpContext context;
        public IdentityController(UserManager<User> userManager, IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.appSettings = appSettings;

        }

        [Route(nameof(Register))]
        public async Task<IActionResult> Register(RegisterUserRequestModel model)
        {
            var user = new User { Email = model.Email, UserName = model.UserName };

            var result = await this.userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors);
        }
        [Route(nameof(Login))]
        public async Task<ActionResult<object>> Login(LoginRequestModel model)
        {

            var user = await this.userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            var passwordValidation = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValidation)
            {
                return Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return new
            {
                Token = encryptedToken
            };


        }

    }
}

