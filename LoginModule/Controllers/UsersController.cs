using LoginModule.Context;
using LoginModule.DTOs;
using LoginModule.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginModule.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private static readonly User _user = new();

        public UsersController(AppDbContext context, IConfiguration configuration) {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("createUser")]
        public ActionResult<User> CreateUser(User request) {
            if (request == null) return BadRequest("[ ERRO ] Faltam informações para o cadastro!");
            var isUserExists = _context.User.FirstOrDefault(u => u.Email == request.Email);

            if (isUserExists is not null) return BadRequest("[ ERRO ] Email já cadastrado!");
            
            string passHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            _user.Id = new Guid();
            _user.FirstName = request.FirstName;
            _user.LastName = request.LastName;
            _user.Email = request.Email;
            _user.Password = passHash;
           

            _context.User.Add(_user);
            _context.SaveChanges();

            return Ok($"Email: {_user.Email}, Senha: {_user.Password}");
        }


        [HttpPost("login")]
        public ActionResult<string> Login(UserDTO request) {
            var userExist = _context.User.FirstOrDefault(u => u.Email == request.Email);

            if (userExist is null) return BadRequest("[ ERRO ] Usuário não encontrado!");
            if (!BCrypt.Net.BCrypt.Verify(request.Password, userExist.Password)) return BadRequest("[ ERRO ] Senha Incorreta");

            string token = CreateToken(request);

            return Ok(token);
        }

        private string CreateToken(UserDTO user) {
            List<Claim> claims = new() {
                new Claim(ClaimTypes.Email, user.Email!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration.GetSection("Jwt:Key").Value!)
                );
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
