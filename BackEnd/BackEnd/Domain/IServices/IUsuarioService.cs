using BackEnd.Domain.Models;
using System.Threading.Tasks;

namespace BackEnd.Domain.IServices
{
    public interface IUsuarioService
    {
        Task SaveUser(Usuario usuario);
        Task<bool> ValidarExistence(Usuario usuario);
    }
}
