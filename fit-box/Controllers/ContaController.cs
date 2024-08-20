using fit_box.Models;
using fit_box.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fit_box.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly LoginService _loginServices;
        private readonly string _chaveSecreta = "e3c46810-b96e-40d9-a9eb-9ffa7b373e5b"; 

        public ContaController(LoginService loginServices)
        {
            _loginServices = loginServices;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] Login login)
        {
            var user = await _loginServices.AuthenticateUserAsync(login.Username, login.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Credenciais inválidas." });
            }

            var token = GerarTokenJWT(user);
            return Ok(new { token });
        }

        private string GerarTokenJWT(Login login)
        {
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_chaveSecreta));
            var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, login.Id.ToString()), 
        new Claim(ClaimTypes.Name, login.Username) 
    };

            var token = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credencial
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
