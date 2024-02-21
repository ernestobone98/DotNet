using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ASP.Server.Models;
using ASP.Server.ViewModels;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace ASP.Server.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly IMapper mapper;

        public BookController(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public ActionResult<IEnumerable<Book>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            var listBooks = _libraryDbContext.Books.Include(b => b.Genres).ToList();
                                        
            return View(listBooks);
        }
        
        public ActionResult<CreateBookViewModel> Create(CreateBookViewModel viewModel, IFormCollection form)
        {
            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (ModelState.IsValid)
            {
                var book = new Book()
                {
                    Name = viewModel.Name,
                    Author = viewModel.Author,
                    Price = viewModel.Price,
                    Content = viewModel.Content,
                    Genres = viewModel.Genres.Select(id => _libraryDbContext.Genre.Find(id)).ToList()
                };
                _libraryDbContext.Books.Add(book);
                _libraryDbContext.SaveChanges();
                return RedirectToAction(nameof(List));
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            viewModel.AllGenres = _libraryDbContext.Genre.ToList();
            return View(viewModel);
        }
    }
}
