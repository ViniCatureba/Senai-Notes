

using Senai_Notas.Models;

namespace Senai_Notas.Interfaces
{
    public interface ITagRepository
    {
        // Retorna todas as tags de forma assíncrona
        Task<IEnumerable<Tag>> ListarTodosAsync();

        // Busca uma tag pelo ID de forma assíncrona
        Task<Tag?> BuscarPorIdAsync(int id);

        // Cadastra uma nova tag de forma assíncrona
        Task CadastrarAsync(Tag tag);

        // Atualiza uma tag existente de forma assíncrona
        Task AtualizarAsync(int id, Tag tag);

        // Remove uma tag de forma assíncrona
        Task DeletarAsync(int id);

        // Retorna todas as tags associadas a um usuário específico
        Task<List<Tag>> BuscarTagsPorUsuarioId(int idUsuario);
    }
}
