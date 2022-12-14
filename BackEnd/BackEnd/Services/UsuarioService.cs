using BackEnd.Domain.IRepositories;
using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task SaveUser(Usuario usuario)
        {
            await _usuarioRepository.SaveUser(usuario);
        }

        public async Task<bool> ValidarExistence(Usuario usuario)
        {
            return await _usuarioRepository.ValidarExistence(usuario);
        }

        public async Task<Usuario> ValidarPassword (int idUsuario, string passwordAnterior)
        {
            return await _usuarioRepository.ValidarPassword(idUsuario, passwordAnterior);
        }

        public async Task UpdatePassword (Usuario usuario)
        {
            await _usuarioRepository.UpdatePassword(usuario);
        }
    }
}
