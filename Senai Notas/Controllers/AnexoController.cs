using Microsoft.AspNetCore.Mvc;
using Senai_Notas.Models;
using Senai_Notas.Interfaces;

namespace Senai_Notas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnexoController : ControllerBase
    {
        private readonly IAnexoRepository _anexoRepository;

        // Construtor com injeção de dependência do repositório
        public AnexoController(IAnexoRepository anexoRepository)
        {
            _anexoRepository = anexoRepository;
        }

        /// <summary>
        /// Retorna todos os anexos cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anexo>>> GetAll()
        {
            var anexos = await _anexoRepository.ListarTodosAsync();
            return Ok(anexos);
        }

        /// Retorna um anexo específico pelo ID.
      
        [HttpGet("{id}")]
        public async Task<ActionResult<Anexo>> GetById(int id)
        {
            var anexo = await _anexoRepository.BuscarPorIdAsync(id);
            if (anexo == null)
                return NotFound();
            return Ok(anexo);
        }

        /// <summary>
        /// Cadastra um novo anexo.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Anexo anexo)
        {
            await _anexoRepository.CadastrarAsync(anexo);
            // Retorna 201 Created com o local do novo recurso
            return CreatedAtAction(nameof(GetById), new { id = anexo.IdAnexo }, anexo);
        }

        /// <summary>
        /// Atualiza um anexo existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Anexo anexo)
        {
            await _anexoRepository.AtualizarAsync(id, anexo);
            return NoContent();
        }

        /// <summary>
        /// Remove um anexo pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _anexoRepository.DeletarAsync(id);
            return NoContent();
        }
    }
}
