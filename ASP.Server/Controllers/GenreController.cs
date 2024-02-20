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
            if (ModelState.IsValid)
            {
                libraryDbContext.Add(new Genre() {  });
                libraryDbContext.SaveChanges();
            }

            return View(new CreateGenreViewModel());
        }
    }
}
