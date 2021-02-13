using FilmsCatalog.Data.Contract.Identity;
using System;

namespace FilmsCatalog.Data.Contract.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IssueYear { get; set; }
        public byte[] Poster { get; set; }
        public string Producer { get; set; }
        public Guid CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}
