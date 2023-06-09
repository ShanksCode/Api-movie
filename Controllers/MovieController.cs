﻿using Api_movie.Models;
using Api_movie.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_movie.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMovieAsync([FromBody] Movie movie)
        {
            movie = await _movieService.AddMovieAsync(movie);
            return Ok($"Filme adicionado com sucesso!" + movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            await _movieService.DeleteMovieAsync(movie);
            return Ok($"Filme deletado com sucesso!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieByIdAsync(int id)
        {
            try
            {
                var movies = await _movieService.GetMovieByIdAsync(id);
                return Ok(movies);

            }
            catch (Exception)
            {
                return NotFound("Filme não encontrado");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies.ToList());
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateMovieAsync(int id, [FromBody] Movie movie)
        {
            await _movieService.UpdateMovieAsync(movie);
            return Ok("Filme editado com sucesso");

        }
    }
}
