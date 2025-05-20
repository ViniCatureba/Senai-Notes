using Senai_Notas.Context;
using Senai_Notas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senai_Notas.Interfaces;

namespace Senai_Notas.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private readonly ProjetoSenaiContext _context;

        public NotaRepository(ProjetoSenaiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Nota>> ListarTodosAsync()
        {
            return await _context.Notas.ToListAsync();
        }

        public async Task<Nota?> BuscarPorIdAsync(int id)
        {
            return await _context.Notas.FindAsync(id);
        }

        public async Task CadastrarAsync(Nota nota)
        {
            await _context.Notas.AddAsync(nota);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(int id, Nota nota)
        {
            var notaExistente = await _context.Notas.FindAsync(id);
            if (notaExistente != null)
            {
                notaExistente.Titulo = nota.Titulo;
                notaExistente.Conteudo = nota.Conteudo;
                notaExistente.UltimoRefresh = nota.UltimoRefresh;
                notaExistente.DataCriacao = nota.DataCriacao;
                notaExistente.Arquivado = nota.Arquivado;
                notaExistente.Status = nota.Status;
                notaExistente.IdUsuario = nota.IdUsuario;
                notaExistente.IdAnexo = nota.IdAnexo;

                _context.Notas.Update(notaExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletarAsync(int id)
        {
            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                _context.Notas.Remove(nota);
                await _context.SaveChangesAsync();
            }
        }

        public Task NotasDeUmUsuario(int id)
        {
            throw new NotImplementedException();
        }
    }
}
