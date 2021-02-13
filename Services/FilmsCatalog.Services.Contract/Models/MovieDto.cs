using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FilmsCatalog.Services.Contract.Models
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Display(Name="Название")]
        [Required(ErrorMessage = "Введите название фильма")]
        public string Name { get; set; }
        [Display(Name="Описание")]
        [Required(ErrorMessage = "Введите описание фильма")]
        public string Description { get; set; }
        [Display(Name="Год выпуска")]
        [Required(ErrorMessage = "Введите год выпуска фильма")]
        [Range(1,9999,ErrorMessage = "Введите корректный год")]
        public int? IssueYear { get; set; }
        [Display(Name="Режиссёр")]
        [Required(ErrorMessage = "Введите имя режиссёра")]
        public string Producer { get; set; }
        [Display(Name="Постер")]
        public IFormFile Image { get; set; }
        public byte[] Poster { get; set; }
        public Guid CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public bool IsOwner { get; set; }
    }
}
