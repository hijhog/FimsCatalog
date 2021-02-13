using FilmsCatalog.Services.Contract.Models;
using System;
using System.Threading.Tasks;

namespace FilmsCatalog.Services.Contract.Interfaces
{
    public interface IMovieService
    {
        Task<PaginationModel<MovieDto>> GetPageAsync(int page);
        Task<MovieDto> Get(int id);
        Task<OperationResult> AddAsync(MovieDto model);
        Task<OperationResult> EditAsync(MovieDto model, Guid userId);
        Task Remove(int id, Guid userId);
    }
}
