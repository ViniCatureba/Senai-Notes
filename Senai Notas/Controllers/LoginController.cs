using Microsoft.AspNetCore.Mvc;
using Senai_Notas.Interfaces;
using Senai_Notas.Services;

namespace Senai_Notas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PasswordService _passwordService;
        private readonly TokenService _tokenService;

        public LoginController(
            IUsuarioRepository usuarioRepository,
            PasswordService passwordService,
            TokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        
        /// Realiza o login do usuário e retorna um token JWT.
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDTO login)
        {
            // Busca o usuário pelo e-mail
            var usuario = await _usuarioRepository.BuscarPorEmailAsync(login.Email);
            // Verifica se o usuário existe e se a senha está correta
            if (usuario == null || !_passwordService.VerifyPassword(login.Senha, usuario.Senha))
                return Unauthorized("Usuário ou senha inválidos");

            // Gera o token JWT
            var token = _tokenService.GenerateToken(usuario.IdUsuario, usuario.Email, usuario.Nome);
            return Ok(new { token });
        }
    }
}
