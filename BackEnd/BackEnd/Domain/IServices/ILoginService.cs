using BackEnd.Domain.Models;
using System.Threading.Tasks;

namespace BackEnd.Domain.IServices
{
    public interface ILoginService
    {
        Task<Usuario> ValidateUser(Usuario usuario);
    }
}
