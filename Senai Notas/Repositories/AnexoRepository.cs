using Senai_Notas.Context;
using Senai_Notas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senai_Notas.Interfaces;

namespace Senai_Notas.Repositories
{
    public class AnexoRepository : IAnexoRepository
    {
        private readonly ProjetoSenaiContext _context;

        public AnexoRepository(ProjetoSenaiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Anexo>> ListarTodosAsync()
        {
            return await _context.Anexos.ToListAsync();
        }

        public async Task<Anexo?> BuscarPorIdAsync(int id)
        {
            return await _context.Anexos.FindAsync(id);
        }

        public async Task CadastrarAsync(Anexo anexo)
        {
            await _context.Anexos.AddAsync(anexo);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(int id, Anexo anexo)
        {
            var anexoExistente = await _context.Anexos.FindAsync(id);
            if (anexoExistente != null)
            {
                anexoExistente.NomeArquivo = anexo.NomeArquivo;
                anexoExistente.DataUpload = anexo.DataUpload;
                anexoExistente.Url = anexo.Url;
                anexoExistente.Arquivo = anexo.Arquivo;

                _context.Anexos.Update(anexoExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletarAsync(int id)
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
