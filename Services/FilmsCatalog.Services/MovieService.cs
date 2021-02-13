using AutoMapper;
using AutoMapper.QueryableExtensions;
using FilmsCatalog.Data;
using FilmsCatalog.Data.Contract.Entities;
using FilmsCatalog.Services.Contract;
using FilmsCatalog.Services.Contract.Interfaces;
using FilmsCatalog.Services.Contract.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsCatalog.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public MovieService(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<MovieDto> Get()
        {
            return _context.Movies.Select(x=>new MovieDto()).ToList();
        }

        public async Task<PaginationModel<MovieDto>> GetPageAsync(int page = 1)
        {
            int pageSize = 5;
            IQueryable<Movie> source = _context.Movies;
            var pageModel = new PageModel(await source.CountAsync(), page, pageSize);
            return new PaginationModel<MovieDto>
            {
                PageModel = pageModel,
                Items = await source.Skip((page - 1) * pageSize)
                              .Take(pageSize).ProjectTo<MovieDto>(_mapper.ConfigurationProvider).ToListAsync()
            };
        }
        
        public async Task<MovieDto> Get(int id)
        {
            return _mapper.Map<MovieDto>(await _context.Movies.FindAsync(id));
        }

        public async Task<OperationResult> AddAsync(MovieDto model)
        {
            var result = new OperationResult();
            try
            {
                var movie = _mapper.Map<Movie>(model);
                if(model.Image != null)
                {
                    movie.Poster = GetByteArrayFromFileImage(model.Image);
                }
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                result.Succeeded = true;
            }
            catch(Exception ex)
            {
                result.ErrorMessage = "Не удалось добавить фильм";
            }
            return result;
        }

        public async Task<OperationResult> EditAsync(MovieDto model, Guid userId)
        {
            var result = new OperationResult();
            try
            {
                var movie = await _context.Movies.FindAsync(model.Id);
                if (movie != null && movie.CreatedById == userId)
                {
                    _mapper.Map(model, movie);
                    if(model.Image != null)
                    {
                        movie.Poster = GetByteArrayFromFileImage(model.Image);
                    }

                    await _context.SaveChangesAsync();
                    result.Succeeded = true;
                }
            }
            catch(Exception ex)
            {
                result.ErrorMessage = "Не удалось отредактировать фильм";
            }
            return result;
        }

        public async Task Remove(int id, Guid userId)
        {
            var movie = await _context.Movies.FindAsync(id);
            if(movie != null && movie.CreatedById == userId)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }

        private byte[] GetByteArrayFromFileImage(Microsoft.AspNetCore.Http.IFormFile imageFile)
        {
            byte[] image = null;
            using (var binaryReader = new BinaryReader(imageFile.OpenReadStream()))
            {
                image = binaryReader.ReadBytes((int)imageFile.Length);
            }
            return image;
        }
    }
}
