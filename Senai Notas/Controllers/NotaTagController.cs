using Microsoft.AspNetCore.Mvc;
using Senai_Notas.Models;
using Senai_Notas.Interfaces;

namespace Senai_Notas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaTagController : ControllerBase
    {
        private readonly INotaTagRepository _notaTagRepository;

        // Construtor com injeção de dependência do repositório
        public NotaTagController(INotaTagRepository notaTagRepository)
        {
            _notaTagRepository = notaTagRepository;
        }

        /// <summary>
        /// Retorna todas as relações NotaTag cadastradas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotaTag>>> GetAll()
        {
            var notaTags = await _notaTagRepository.ListarTodosAsync();
            return Ok(notaTags);
        }

        /// <summary>
        /// Retorna uma relação NotaTag específica pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<NotaTag>> GetById(int id)
        {
            var notaTag = await _notaTagRepository.BuscarPorIdAsync(id);
            if (notaTag == null)
                return NotFound();
            return Ok(notaTag);
        }

        /// <summary>
        /// Cadastra uma nova relação NotaTag.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] NotaTag notaTag)
        {
            await _notaTagRepository.CadastrarAsync(notaTag);
            // Retorna 201 Created com o local do novo recurso
            return CreatedAtAction(nameof(GetById), new { id = notaTag.IdNotaTag }, notaTag);
        }

        /// <summary>
        /// Atualiza uma relação NotaTag existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] NotaTag notaTag)
        {
            await _notaTagRepository.AtualizarAsync(id, notaTag);
            return NoContent();
        }

        /// <summary>
        /// Remove uma relação NotaTag pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _notaTagRepository.DeletarAsync(id);
            return NoContent();
        }
    }
}
