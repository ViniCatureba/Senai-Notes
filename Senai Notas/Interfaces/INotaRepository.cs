using Senai_Notas.Models;

namespace Senai_Notas.Interfaces
{
    public interface INotaRepository
    {
        // Retorna todas as notas de forma assíncrona
        Task<IEnumerable<Nota>> ListarTodosAsync();

        // Busca uma nota pelo ID de forma assíncrona
        Task<Nota?> BuscarPorIdAsync(int id);

        // Cadastra uma nova nota de forma assíncrona
        Task CadastrarAsync(Nota nota);

        // Atualiza uma nota existente de forma assíncrona
        Task AtualizarAsync(int id, Nota nota);

        // Remove uma nota de forma assíncrona
        Task DeletarAsync(int id);
    }
}
