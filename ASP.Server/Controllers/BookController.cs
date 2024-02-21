using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ASP.Server.Models;
using ASP.Server.ViewModels;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using NSwag.Annotations;

namespace ASP.Server.Controllers
{
    public class BookController(LibraryDbContext libraryDbContext, IMapper mapper) : Controller
    {
        private readonly LibraryDbContext libraryDbContext = libraryDbContext;

        public ActionResult<IEnumerable<Book>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            IEnumerable<Book> ListBooks = libraryDbContext.Books.Include(b => b.Author)
                .Include(b => b.Genres);
            return View(ListBooks);
        }

        public ActionResult<CreateBookViewModel> Create(CreateBookViewModel book)
        {
            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (ModelState.IsValid)
            {
                // Completer la création du livre avec toute les information nécéssaire que vous aurez ajoutez, et metter la liste des gener récupéré de la base aussi
                libraryDbContext.Add(new Book() {  });
                libraryDbContext.SaveChanges();
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateBookViewModel() { AllGenres = libraryDbContext.Genre});
        }
        
        [HttpPost("/delete/{id}")]
        [OpenApiIgnore]
        public ActionResult Delete(int id)
        {
            var bookToDelete = libraryDbContext.Books.SingleOrDefault(b => b.Id == id);
            if (bookToDelete == null) return NotFound();
            libraryDbContext.Books.Remove(bookToDelete);
            libraryDbContext.SaveChanges();
            return RedirectToAction("List");
        }
        
        [HttpGet("/Book/update/{id}")]
        [OpenApiIgnore]
        public ActionResult Update(int id)
        {
            var book = libraryDbContext.Books.Include(b => b.Genres).Include(b => b.Author).SingleOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            var bookToUpdate = new UpdateBookViewModel();
            bookToUpdate.Genres = book.Genres;
            bookToUpdate.Author = book.Author;
            bookToUpdate.Content = book.Content;
            bookToUpdate.Name = book.Name;
            bookToUpdate.Id = book.Id;
            bookToUpdate.Price = book.Price;
            bookToUpdate.AllGenres = libraryDbContext.Genre;
            return View(bookToUpdate);
        }
        
        public ActionResult Update1(Update1BookViewModel updateBook, IFormCollection form)
        {
            if (!ModelState.IsValid)
            {
                // Si le modèle n'est pas valide, retourner le formulaire avec les erreurs de validation
                return NotFound(); //View("Update", updatedBook);
            }
            
            var id = int.Parse(form["Id"]);
            var name = form["Name"].ToString();
            var author = form["Author"].ToString();
            var genres =  form["Genres"].ToString().Split(',');
            var price = float.Parse(form["Price"]);
            var content = form["Content"].ToString();

            var book = libraryDbContext.Books.Include(b => b.Author.Books).Include(b => b.Genres).SingleOrDefault(b => b.Id == id);
            book.Name = name;
            book.Price = price;
            book.Content = content;

            if (author != book.Author.Name)
            {
                var oldAuthor = book.Author;
                var authorQuery = libraryDbContext.Author.Include(a => a.Books).Where(a => a.Name == author);
                if (authorQuery.Any()) // if exist
                {
                    book.Author = authorQuery.FirstOrDefault(a => a.Name == author);
                }
                else // create entity
                {
                    libraryDbContext.Author.Add(new Author() { Name = author, Books = [book] });
                }
                oldAuthor.Books = oldAuthor.Books.Where(b => b.Name != name);
                Console.WriteLine($"DEBUG : new {book.Author.Name} old {oldAuthor.Name}");
            }

            var genresQuery = libraryDbContext.Genre.Include(g => g.Books);
            if (genresQuery.Any()) // if exist
            {
                var newGenreList = genresQuery.Where(g => genres.Contains($"{g.Id}")).ToList();
                book.Genres = newGenreList;
            }

            libraryDbContext.SaveChanges();

            // Rediriger vers l'action List pour afficher la liste mise à jour des livres
            return RedirectToAction("List");
        }

        
    }
}
