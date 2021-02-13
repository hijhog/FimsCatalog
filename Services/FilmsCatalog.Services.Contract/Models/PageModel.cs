using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsCatalog.Services.Contract.Models
{
    public class PageModel
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public PageModel(int count, int pageNumber, int pageSize = 5)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get { return PageNumber > 1; }
        }

        public bool HasNextPage
        {
            get { return PageNumber < TotalPages; }
        }
    }
}
