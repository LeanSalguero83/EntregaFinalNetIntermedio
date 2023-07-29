using Data;
using Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Helpers;

namespace Api.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticateController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto usuario)
        {
            usuario.Clave = EncryptHelper.Encriptar(usuario.Clave);

			var validarUsuario = _context.Usuarios.Where(x => x.Mail == usuario.Mail && x.Clave == usuario.Clave).Include(x => x.Roles).FirstOrDefault();

			if (validarUsuario != null)
            {
                var Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, validarUsuario.Mail),
                    new Claim(ClaimTypes.DateOfBirth, validarUsuario.Fecha_Nacimiento.ToString()),
                    new Claim(ClaimTypes.Role, validarUsuario.Roles.Nombre),
                };

                var token = CrearToken(Claims);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token).ToString() + ";" + validarUsuario.Nombre + ";" + validarUsuario.Roles.Nombre + ";" + validarUsuario.Mail);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("LoginGoogle")]
        public async Task<IActionResult> LoginGoogle(LoginDto usuario)
        {
            var validarUsuario = _context.Usuarios.Where(x => x.Mail == usuario.Mail).Include(x => x.Roles).FirstOrDefault();

            if (validarUsuario != null)
            {
                var Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, validarUsuario.Mail),
                    new Claim(ClaimTypes.DateOfBirth, validarUsuario.Fecha_Nacimiento.ToString()),
                    new Claim(ClaimTypes.Role, validarUsuario.Roles.Nombre),
                };

                var token = CrearToken(Claims);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token).ToString() + ";" + validarUsuario.Nombre + ";" + validarUsuario.Roles.Nombre + ";" + validarUsuario.Mail);
            }
            else
            {
                return Unauthorized();
            }
        }

        private JwtSecurityToken CrearToken(List<Claim> autorizar)
        {
            try
            {
                var firma = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Firma"]));

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(24),
                    claims: autorizar,
                    signingCredentials: new SigningCredentials(firma, SecurityAlgorithms.HmacSha256)
                    );

                return token;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

    
