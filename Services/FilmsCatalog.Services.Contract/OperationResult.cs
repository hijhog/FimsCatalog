using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsCatalog.Services.Contract
{
    public class OperationResult
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
