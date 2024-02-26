using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ASP.Server.Models;

namespace ASP.Server.Database
{
    public class DbInitializer
    {
        public static void Initialize(LibraryDbContext bookDbContext)
        {
            if (bookDbContext.Books.Any())
                return;
            
            Author jkRolling;
            bookDbContext.Author.AddRange(
                jkRolling = new Author()
                {
                    Name = "J.K. Rowling",
                    Books = new List<Book>()
                });
            bookDbContext.SaveChanges();
            
            Genre SF, Classic, Romance, Thriller;
            bookDbContext.Genre.AddRange(
                SF = new Genre() { Name = "SF"},
                Classic = new Genre() { Name = "Classic"},
                Romance = new Genre() { Name = "Romance"},
                Thriller = new Genre() { Name = "Thriller"}
            );
            bookDbContext.SaveChanges();
            
            // Vous pouvez initialiser la BDD ici
            bookDbContext.Books.AddRange(
                               new Book()
                               {
                                   Author = jkRolling,
                                   Name = "Harry Potter and the Philosopher's Stone",
                                   Price = 9.99f,
                                   Content = "Harry Potter has never even heard of Hogwarts when the " +
                                   "letters start dropping on the doormat at number four, Privet Drive. Addressed in " +
                                   "green ink on yellowish parchment with a purple seal, they are swiftly confiscated by " +
                                   "his grisly aunt and uncle. Then, on Harry's eleventh birthday, a great beetle-eyed",
                                   Genres = new List<Genre>() { SF, Classic }
                               });
            bookDbContext.Books.AddRange(
                               new Book()
                               {
                                   Author = jkRolling,
                                   Name = "Harry Potter and the Chamber of Secrets",
                                   Price = 9.99f,
                                   Content = "The Dursleys were so mean and hideous that summer that all Harry Potter wanted " +
                                   "was to get back to the Hogwarts School for Witchcraft and Wizardry. But just as he's " +
                                   "packing his bags, Harry receives a warning from a strange, impish creature named Dobby " +
                                   "who says that if Harry Potter returns to Hogwarts, disaster will strike.",
                                   Genres = new List<Genre>() { SF, Classic }
                               });

            bookDbContext.Books.AddRange(
                               new Book()
                               {
                                   Author = jkRolling,
                                   Name = "Harry Potter and the Prisoner of Azkaban",
                                   Price = 9.99f,
                                   Content = "Harry Potter is lucky to reach the age of thirteen, since he has already survived" +
                                   " the murderous attacks of the feared Dark Lord on more than one occasion. But his hopes " +
                                   "for a quiet term concentrating on Quidditch are dashed when a maniacal mass",
                                   Genres = new List<Genre>() { SF, Classic }
                               });
            

            bookDbContext.SaveChanges();
        }
    }
}