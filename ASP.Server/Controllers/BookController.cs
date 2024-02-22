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

        public ActionResult<CreateBookViewModel> Create(CreateBookViewModel viewModel)
        { // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (ModelState.IsValid)
            {
                var author = libraryDbContext.Author.Include(a => a.Books).Where(a => a.Name == viewModel.Author);
                Author newAuthor;
                if (author.Any()) //if exist
                {
                    newAuthor = author.FirstOrDefault();
                }
                else //creation of a new autor
                {
                    libraryDbContext.Add(new Author() { Name = viewModel.Author });
                    libraryDbContext.SaveChanges();
                    newAuthor = author.FirstOrDefault();
                }

                var book = new Book()
                {
                    Name = viewModel.Name,
                    Author = newAuthor,
                    Price = viewModel.Price,
                    Content = viewModel.Content,
                    Genres = viewModel.Genres.Select(id => libraryDbContext.Genre.Find(id)).ToList()
                };
                libraryDbContext.Add(book);
                libraryDbContext.SaveChanges();
                newAuthor.Books = newAuthor.Books.Append(book);
                return RedirectToAction(nameof(List));
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            viewModel.AllGenres = libraryDbContext.Genre;
            return View(viewModel);
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
        
        public ActionResult Update1(Update1BookViewModel updateBook)
        {
            if (!ModelState.IsValid)
            {
                // Si le modèle n'est pas valide, retourner le formulaire avec les erreurs de validation
                return NotFound(); //View("Update", updatedBook);
            }
            
            Console.WriteLine("GONNA WRITE:");
            Console.WriteLine($"\n\n{updateBook.Id}, {updateBook.Name}, {updateBook.Genres}, {updateBook.Price}, {updateBook.Content}");

            var id = updateBook.Id;
            var name = updateBook.Name;
            var author = updateBook.Author;
            var genres = updateBook.Genres;//form["Genres"].ToString().Split(',');
            var price = updateBook.Price;
            var content = updateBook.Content;

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
                var newGenreList = genresQuery.Where(g => genres.Contains(g.Id)).ToList();
                book.Genres = newGenreList;
            }

            libraryDbContext.SaveChanges();

            // Rediriger vers l'action List pour afficher la liste mise à jour des livres
            return RedirectToAction("List");
        }

        
    }
}
