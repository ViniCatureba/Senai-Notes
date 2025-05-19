using Senai_Notas.Context;
using Senai_Notas.Models;
using Senai_Notas.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senai_Notas.Interfaces;
using Senai_Notas.Services;
using Microsoft.AspNetCore.Mvc;

namespace Senai_Notas.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ProjetoSenaiContext _context;

        public UsuarioRepository(ProjetoSenaiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> ListarTodosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> BuscarPorIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario?> BuscarPorEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task CadastrarAsync(CadastrarUsuarioDTO usuarioDTO)
        {
            var passwordService = new PasswordService();
            var usuarioCadastro = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
            };
            usuarioCadastro.Senha = passwordService.HashPassword(usuarioDTO.Senha);
            await _context.Usuarios.AddAsync(usuarioCadastro);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(int id, Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Email = usuario.Email;
                usuarioExistente.Senha = usuario.Senha;
                usuarioExistente.UrlFoto = usuario.UrlFoto;
                usuarioExistente.Tema = usuario.Tema;
                usuarioExistente.Fonte = usuario.Fonte;

                _context.Usuarios.Update(usuarioExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletarAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

       

        
    }
}
