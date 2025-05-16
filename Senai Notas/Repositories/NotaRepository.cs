using Senai_Notas.Context;
using Senai_Notas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senai_Notas.Repositories
{
    public class NotaRepository
    {
        private readonly ProjetoSenaiContext _context;

        public NotaRepository(ProjetoSenaiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Nota>> ListarAsync()
        {
            return await _context.Notas.ToListAsync();
        }

        public async Task<Nota?> BuscarPorIdAsync(int id)
        {
            return await _context.Notas.FindAsync(id);
        }

        public async Task AdicionarAsync(Nota nota)
        {
            await _context.Notas.AddAsync(nota);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Nota nota)
        {
            _context.Notas.Update(nota);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                _context.Notas.Remove(nota);
                await _context.SaveChangesAsync();
            }
        }
    }
}
