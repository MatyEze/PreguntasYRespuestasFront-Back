using BackEnd.Domain.IRepositories;
using BackEnd.Domain.Models;
using BackEnd.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BackEnd.Persistence.Repositories
{
    public class UsuarioRepositiry: IUsuarioRepository
    {
        private readonly AplicationDbContext _context;
        public UsuarioRepositiry(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task SaveUser(Usuario usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidarExistence(Usuario usuario)
        {
            var validarExistence = await _context.Usuarios.AnyAsync(x => x.NombreUsuario == usuario.NombreUsuario);
            return validarExistence;
        }
    }
}
