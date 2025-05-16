
using Senai_Notas.Models;

namespace Senai_Notas.Interfaces
{
    public interface IUsuarioRepository
    {
        // Retorna todos os usuários de forma assíncrona
        Task<IEnumerable<Usuario>> ListarTodosAsync();

        // Busca um usuário pelo ID de forma assíncrona
        Task<Usuario?> BuscarPorIdAsync(int id);

        // Cadastra um novo usuário de forma assíncrona
        Task CadastrarAsync(Usuario usuario);

        // Atualiza um usuário existente de forma assíncrona
        Task AtualizarAsync(int id, Usuario usuario);

        // Remove um usuário de forma assíncrona
        Task DeletarAsync(int id);
    }
}
