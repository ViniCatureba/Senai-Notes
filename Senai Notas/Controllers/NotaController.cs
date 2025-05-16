using Microsoft.AspNetCore.Mvc;
using Senai_Notas.Models;
using Senai_Notas.Interfaces;

namespace Senai_Notas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private readonly INotaRepository _notaRepository;

        // Construtor com injeção de dependência do repositório
        public NotaController(INotaRepository notaRepository)
        {
            _notaRepository = notaRepository;
        }

        /// <summary>
        /// Retorna todas as notas cadastradas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nota>>> GetAll()
        {
            var notas = await _notaRepository.ListarTodosAsync();
            return Ok(notas);
        }

        /// <summary>
        /// Retorna uma nota específica pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Nota>> GetById(int id)
        {
            var nota = await _notaRepository.BuscarPorIdAsync(id);
            if (nota == null)
                return NotFound();
            return Ok(nota);
        }

        /// <summary>
        /// Cadastra uma nova nota.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Nota nota)
        {
            await _notaRepository.CadastrarAsync(nota);
            // Retorna 201 Created com o local do novo recurso
            return CreatedAtAction(nameof(GetById), new { id = nota.IdNota }, nota);
        }

        /// <summary>
        /// Atualiza uma nota existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Nota nota)
        {
            await _notaRepository.AtualizarAsync(id, nota);
            return NoContent();
        }

        /// <summary>
        /// Remove uma nota pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _notaRepository.DeletarAsync(id);
            return NoContent();
        }
    }
}
