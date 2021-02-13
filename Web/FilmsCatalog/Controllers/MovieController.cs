using FilmsCatalog.Extensions;
using FilmsCatalog.Services.Contract.Interfaces;
using FilmsCatalog.Services.Contract.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(
            IMovieService movieService)
        {
            _movieService = movieService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            var model = await _movieService.GetPageAsync(page);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.CreatedById = User.GetUserId().Value;
            var result = await _movieService.AddAsync(model);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _movieService.Get(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _movieService.EditAsync(model, User.GetUserId().Value);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            await _movieService.Remove(id, User.GetUserId().Value);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _movieService.Get(id);
            return View(model);
        }
    }
}
