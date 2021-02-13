using AutoMapper;
using FilmsCatalog.Data.Contract.Entities;
using FilmsCatalog.Services.Contract.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsCatalog.Services.AutoMapper
{
    public class MovieMapper : Profile
    {
        public MovieMapper()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(dest=>dest.CreatedBy, opt=>opt.MapFrom(src=>$"{src.CreatedBy.LastName} {src.CreatedBy.FirstName}"));
            CreateMap<MovieDto, Movie>()
                .ForMember(dest=>dest.Id, opt=>opt.Ignore())
                .ForMember(dest=>dest.CreatedById, opt=>opt.Ignore())
                .ForMember(dest=>dest.Poster, opt=>opt.Ignore());
        }
    }
}
