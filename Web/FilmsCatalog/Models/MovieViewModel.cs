namespace FilmsCatalog.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IssueYear { get; set; }
        public string Producer { get; set; }
        public string CreatedBy { get; set; }
        public bool IsOwner { get; set; }
    }
}
