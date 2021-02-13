using FilmsCatalog.Data.Contract.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FilmsCatalog.Data.Contract.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        
        public virtual ICollection<Movie> Movies { get; set; }
    }
}