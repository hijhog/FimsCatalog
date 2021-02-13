using FilmsCatalog.Services.Contract.Models.Account;
using System.Threading.Tasks;

namespace FilmsCatalog.Services.Contract.Interfaces
{
    public interface IAccountService
    {
        Task<OperationResult> CreateAsync(UserDto model);
        Task<OperationResult> LoginAsync(LoginDto model);
        Task LogoutAsync();
    }
}
