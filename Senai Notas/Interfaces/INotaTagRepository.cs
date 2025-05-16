
using Senai_Notas.Models;

namespace Senai_Notas.Interfaces
{
    public interface INotaTagRepository
    {
        // Retorna todas as relações NotaTag de forma assíncrona
        Task<IEnumerable<NotaTag>> ListarTodosAsync();

        // Busca uma relação NotaTag pelo ID de forma assíncrona
        Task<NotaTag?> BuscarPorIdAsync(int id);

        // Cadastra uma nova relação NotaTag de forma assíncrona
        Task CadastrarAsync(NotaTag notaTag);

        // Atualiza uma relação NotaTag existente de forma assíncrona
        Task AtualizarAsync(int id, NotaTag notaTag);

        // Remove uma relação NotaTag de forma assíncrona
        Task DeletarAsync(int id);
    }
}
