using BackEnd.Domain.Models;
using System.Threading.Tasks;

namespace BackEnd.Domain.IRepositories
{
    public interface IUsuarioRepository
    {
        Task SaveUser(Usuario usuario);
        Task<bool> ValidarExistence(Usuario usuario);
    }
}
