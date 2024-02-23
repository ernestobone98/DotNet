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
            IEnumerable<Book> listBooks = libraryDbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .ToList();

            return View(listBooks);
        }

        public ActionResult<CreateBookViewModel> Create(CreateBookViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Split the comma-separated string of authors into a list of authors
                var authors = viewModel.Authors
                    .Split(',')
                    .Select(authorName => authorName.Trim())
                    .ToList();

                // Create a list of Author entities for the book
                var authorEntities = authors.Select(authorName => new Author { Name = authorName }).ToList();

                var book = new Book()
                {
                    Name = viewModel.Name,
                    Authors = authorEntities,
                    Price = viewModel.Price,
                    Content = viewModel.Content,
                    Genres = viewModel.Genres.Select(id => libraryDbContext.Genre.Find(id)).ToList()
                };

                libraryDbContext.Add(book);
                libraryDbContext.SaveChanges();

                return RedirectToAction(nameof(List));
            }

            // Interrogate the database to retrieve all genres for user selection
            viewModel.AllGenres = libraryDbContext.Genre;
            return View(viewModel);
        }


        [HttpPost("/Book/delete/{id}")]
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
            var book = libraryDbContext.Books
                .Include(b => b.Genres)
                .Include(b => b.Authors)
                .SingleOrDefault(b => b.Id == id);

            if (book == null) return NotFound();

            var bookToUpdate = new UpdateBookViewModel
            {
                Genres = book.Genres,
                Authors = book.Authors.Select(a => a.Name),
                Content = book.Content,
                Name = book.Name,
                Id = book.Id,
                Price = book.Price,
                AllGenres = libraryDbContext.Genre
            };

            return View(bookToUpdate);
        }

        public ActionResult Update1(int id, Update1BookViewModel updateBook)
        {
            if (!ModelState.IsValid)
            {
                // If the model is not valid, return the form with validation errors
                updateBook.AllGenres = libraryDbContext.Genre;
                return View(updateBook);
            }

            var name = updateBook.Name;
            var authors = updateBook.Authors?
                .Split(',')
                .Select(authorName => authorName.Trim())
                .ToList()
                .Select(authorName => new Author { Name = authorName }).ToList();

            var genres = updateBook.Genres;
            var price = updateBook.Price;
            var content = updateBook.Content;

            var book = libraryDbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .SingleOrDefault(b => b.Id == id);

            book.Name = name;
            book.Price = price;
            book.Content = content;

            if (authors != null)
            {
                // Update or add new authors
                book.Authors = authors.Select(author =>
                {
                    var existingAuthor = libraryDbContext.Author.FirstOrDefault(a => a.Name == author.Name);
                    return existingAuthor ?? new Author { Name = author.Name };
                }).ToList();
            }

            if (genres != null)
            {
                // Update genres
                book.Genres = genres.Select(genreId => libraryDbContext.Genre.Find(genreId)).ToList();
            }

            libraryDbContext.SaveChanges();

            // Redirect to the List action to display the updated list of books
            return RedirectToAction("List");
        }



    }
}
