using Senai_Notas.Models;

namespace Senai_Notas.Interfaces
{
    public interface IAnexoRepository
    {
        // Retorna todos os anexos de forma assíncrona
        Task<IEnumerable<Anexo>> ListarTodosAsync();

        // Busca um anexo pelo ID de forma assíncrona
        Task<Anexo?> BuscarPorIdAsync(int id);

        // Cadastra um novo anexo de forma assíncrona
        Task CadastrarAsync(Anexo anexo);

        // Atualiza um anexo existente de forma assíncrona
        Task AtualizarAsync(int id, Anexo anexo);

        // Remove um anexo de forma assíncrona
        Task DeletarAsync(int id);
    }
}