using System.Collections.Generic;

namespace FilmsCatalog.Services.Contract.Models
{
    public class PaginationModel<TDto> where TDto : class
    {
        public IEnumerable<TDto> Items { get; set; }
        public PageModel PageModel { get; set; }
    }
}
