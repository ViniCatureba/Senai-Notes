using Senai_Notas.Context;
using Senai_Notas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senai_Notas.Interfaces;

namespace Senai_Notas.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ProjetoSenaiContext _context;

        public TagRepository(ProjetoSenaiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> ListarTodosAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag?> BuscarPorIdAsync(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task CadastrarAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(int id, Tag tag)
        {
            var tagExistente = await _context.Tags.FindAsync(id);
            if (tagExistente != null)
            {
                tagExistente.Nome = tag.Nome;
                _context.Tags.Update(tagExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletarAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<Tag>> BuscarTagsPorUsuarioId(int idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
