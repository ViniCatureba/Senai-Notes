using Senai_Notas.Context;
using Senai_Notas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senai_Notas.Repositories
{
    public class AnexoRepository
    {
        private readonly ProjetoSenaiContext _context;

        public AnexoRepository(ProjetoSenaiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Anexo>> ListarAsync()
        {
            return await _context.Anexos.ToListAsync();
        }

        public async Task<Anexo> BuscarPorIdAsync(int id)
        {
            return await _context.Anexos.FindAsync(id);
        }

        public async Task AdicionarAsync(Anexo anexo)
        {
            await _context.Anexos.AddAsync(anexo);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Anexo anexo)
        {
            _context.Anexos.Update(anexo);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var anexo = await _context.Anexos.FindAsync(id);
            if (anexo != null)
            {
                _context.Anexos.Remove(anexo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
