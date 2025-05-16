using Microsoft.AspNetCore.Mvc;
using Senai_Notas.Models;
using Senai_Notas.Interfaces;

namespace Senai_Notas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        // Construtor com injeção de dependência do repositório
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var usuarios = await _usuarioRepository.ListarTodosAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Retorna um usuário específico pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _usuarioRepository.BuscarPorIdAsync(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Usuario usuario)
        {
            await _usuarioRepository.CadastrarAsync(usuario);
            // Retorna 201 Created com o local do novo recurso
            return CreatedAtAction(nameof(GetById), new { id = usuario.IdUsuario }, usuario);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            await _usuarioRepository.AtualizarAsync(id, usuario);
            return NoContent();
        }

        /// <summary>
        /// Remove um usuário pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _usuarioRepository.DeletarAsync(id);
            return NoContent();
        }
    }
}
