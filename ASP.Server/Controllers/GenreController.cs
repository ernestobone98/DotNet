using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ASP.Server.Models;
using NSwag.Annotations;

namespace ASP.Server.Controllers
{
    public class GenreController(LibraryDbContext libraryDbContext, IMapper mapper) : Controller
    {
        private readonly LibraryDbContext libraryDbContext = libraryDbContext;
        private readonly IMapper mapper = mapper;

        // A vous de faire comme BookController.List mais pour les genres !

        public ActionResult<IEnumerable<Genre>> List()
        {
            IEnumerable<Genre> ListGenres = libraryDbContext.Genre.Include(p => p.Books);
            return View(ListGenres);
        }

        public ActionResult<CreateGenreViewModel> Create(CreateGenreViewModel genre)
        {
            genre.AllBooks = libraryDbContext.Books;
            
            if (ModelState.IsValid)
            {
                var selectedBookIds = genre.Books;
                var selectedBooks = libraryDbContext.Books.Where(b => selectedBookIds.Contains(b.Id)).ToList();
                libraryDbContext.Add(new Genre() { 
                    Name = genre.Name,
                    Books = selectedBooks
            });
                libraryDbContext.SaveChanges();
                return RedirectToAction("List");
            }

            return View(new CreateGenreViewModel()
            {
                AllBooks = libraryDbContext.Books
            });
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetMatchingBooks(string filterValue)
        {
            if (string.IsNullOrEmpty(filterValue))
            {
                return Json(new List<Book>());
            }
            else 
            {
                var matchingBooks = libraryDbContext.Books
                .Where(b => b.Name.ToLower().Contains(filterValue.ToLower()))
                .ToList();

                return Json(matchingBooks);
            }
        }


        // Delete method
        [HttpPost("delete/{id}")]
        [OpenApiIgnore]
        public ActionResult<Genre> Delete(int id)
        {
			Genre genre = libraryDbContext.Genre.Find(id);
			if (genre == null)
            {
				return NotFound();
			}

			libraryDbContext.Genre.Remove(genre);
			libraryDbContext.SaveChanges();
			return RedirectToAction("List");
		}
    }
}
