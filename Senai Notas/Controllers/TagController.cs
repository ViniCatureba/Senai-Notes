using Microsoft.AspNetCore.Mvc;
using Senai_Notas.Models;
using Senai_Notas.Interfaces;

namespace Senai_Notas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        // Construtor com injeção de dependência do repositório
        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

    
        /// Retorna todas as tags cadastradas.
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetAll()
        {
            var tags = await _tagRepository.ListarTodosAsync();
            return Ok(tags);
        }

        
        /// Retorna uma tag específica pelo ID.
      
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetById(int id)
        {
            var tag = await _tagRepository.BuscarPorIdAsync(id);
            if (tag == null)
                return NotFound();
            return Ok(tag);
        }

       
        /// Cadastra uma nova tag.
      
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Tag tag)
        {
            await _tagRepository.CadastrarAsync(tag);
            // Retorna 201 Created com o local do novo recurso
            return CreatedAtAction(nameof(GetById), new { id = tag.IdTag }, tag);
        }

      
        /// Atualiza uma tag existente.
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Tag tag)
        {
            await _tagRepository.AtualizarAsync(id, tag);
            return NoContent();
        }

      
        /// Remove uma tag pelo ID.
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _tagRepository.DeletarAsync(id);
            return NoContent();
        }
    }
}
