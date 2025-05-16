using Senai_Notas.Context;
using Senai_Notas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senai_Notas.Interfaces;

namespace Senai_Notas.Repositories
{
    public class NotaTagRepository : INotaTagRepository
    {
        private readonly ProjetoSenaiContext _context;

        public NotaTagRepository(ProjetoSenaiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotaTag>> ListarTodosAsync()
        {
            return await _context.NotaTags.ToListAsync();
        }

        public async Task<NotaTag?> BuscarPorIdAsync(int id)
        {
            return await _context.NotaTags.FindAsync(id);
        }

        public async Task CadastrarAsync(NotaTag notaTag)
        {
            await _context.NotaTags.AddAsync(notaTag);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(int id, NotaTag notaTag)
        {
            var notaTagExistente = await _context.NotaTags.FindAsync(id);
            if (notaTagExistente != null)
            {
                notaTagExistente.IdNota = notaTag.IdNota;
                notaTagExistente.IdTag = notaTag.IdTag;
                _context.NotaTags.Update(notaTagExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletarAsync(int id)
        {
            var notaTag = await _context.NotaTags.FindAsync(id);
            if (notaTag != null)
            {
                _context.NotaTags.Remove(notaTag);
                await _context.SaveChangesAsync();
            }
        }
    }
}
