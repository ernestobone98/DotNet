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

            Author jkRolling, julesVerne, agathaChristie, georgeOrwell, janeAusten, stephenKing, markTwain, ernestHemingway, fScottFitzgerald;
            bookDbContext.Author.AddRange(
                jkRolling = new Author() { Name = "J.K. Rowling" },
                julesVerne = new Author() { Name = "Jules Verne" },
                agathaChristie = new Author() { Name = "Agatha Christie" },
                georgeOrwell = new Author() { Name = "George Orwell" },
                janeAusten = new Author() { Name = "Jane Austen" },
                stephenKing = new Author() { Name = "Stephen King" },
                markTwain = new Author() { Name = "Mark Twain" },
                ernestHemingway = new Author() { Name = "Ernest Hemingway" },
                fScottFitzgerald = new Author() { Name = "Scott Fitzgerald " }
                );
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
                                   Authors = new List<Author>() { jkRolling },
                                   Name = "Harry Potter and the Philosopher's Stone",
                                   Price = 9.99f,
                                   Content = "Harry Potter has never even heard of Hogwarts when the " +
                                   "letters start dropping on the doormat at number four, Privet Drive. Addressed in " +
                                   "green ink on yellowish parchment with a purple seal, they are swiftly confiscated by " +
                                   "his grisly aunt and uncle. Then, on Harry's eleventh birthday, a great beetle-eyed",
                                   Genres = new List<Genre>() { SF, Classic }
                               },
                               new Book()
                               {
                                   Authors = new List<Author>() { julesVerne, jkRolling },
                                   Name = "Harry Potter and the Chamber of Secrets",
                                   Price = 9.99f,
                                   Content = "The Dursleys were so mean and hideous that summer that all Harry Potter wanted " +
                                   "was to get back to the Hogwarts School for Witchcraft and Wizardry. But just as he's " +
                                   "packing his bags, Harry receives a warning from a strange, impish creature named Dobby " +
                                   "who says that if Harry Potter returns to Hogwarts, disaster will strike.",
                                   Genres = new List<Genre>() { SF, Classic }
                               },
                               new Book()
                               {
                                   Authors = new List<Author>() { jkRolling },
                                   Name = "Harry Potter and the Prisoner of Azkaban",
                                   Price = 9.99f,
                                   Content = "Harry Potter is lucky to reach the age of thirteen, since he has already survived" +
                                   " the murderous attacks of the feared Dark Lord on more than one occasion. But his hopes " +
                                   "for a quiet term concentrating on Quidditch are dashed when a maniacal mass",
                                   Genres = new List<Genre>() { SF, Classic }
                               },
                               new Book()
                               {
                                   Authors = new List<Author>() { julesVerne },
                                   Name = "Journey to the Center of the Earth",
                                   Price = 9.99f,
                                   Content = "Journey to the Center of the Earth is a classic science fiction novel by Jules Verne, " +
                                   "published in 1864. It is the story of German professor Otto Lidenbrock, his nephew Axel, and their " +
                                   "guide Hans, who descend into an Icelandic volcano to reach the earth's center.",
                                   Genres = new List<Genre>() { SF, Classic }
                               },
                               new Book()
                               {
                                   Authors = new List<Author> { julesVerne },
                                   Name = "Twenty Thousand Leagues Under the Sea",
                                   Price = 12.99f,
                                   Content = "Professor Pierre Aronnax embarks on an expedition to hunt a mysterious " +
                                      "sea monster that has been terrorizing shipping lanes. However, the creature " +
                                      "turns out to be the submarine Nautilus, commanded by the enigmatic Captain Nemo.",
                                   Genres = new List<Genre> { SF, Classic }
                               },
                                new Book()
                                {
                                    Authors = new List<Author> { agathaChristie },
                                    Name = "Murder on the Orient Express",
                                    Price = 14.99f,
                                    Content = "Detective Hercule Poirot is traveling on the luxurious Orient Express " +
                                              "when a wealthy American is murdered. With a train full of suspects, Poirot " +
                                              "must unravel the mystery and identify the killer.",
                                    Genres = new List<Genre> { Thriller, Classic }
                                },
                                new Book()
                                {
                                    Authors = new List<Author> { ernestHemingway, fScottFitzgerald },
                                    Name = "The Great Gatsby",
                                    Price = 10.99f,
                                    Content = "Jay Gatsby, a mysterious millionaire, pursues his lost love, Daisy Buchanan, amidst the " +
                                    "glamour and excess of the Roaring Twenties.",
                                    Genres = new List<Genre> { Romance }
                                },
                                new Book()
                                {
                                    Authors = new List<Author> { georgeOrwell },
                                    Name = "1984",
                                    Price = 14.99f,
                                    Content = "In a dystopian society, Winston Smith struggles against the omnipresent surveillance of Big Brother.",
                                    Genres = new List<Genre> { SF, Thriller }
                                },
                                new Book()
                                {
                                    Authors = new List<Author> { markTwain, janeAusten },
                                    Name = "Pride and Prejudice Adventures",
                                    Price = 11.99f,
                                    Content = "In this imaginative crossover, characters from Jane Austen's 'Pride and Prejudice' find themselves on a Mississippi River adventure written by Mark Twain.",
                                    Genres = new List<Genre> { Classic, Romance }
                                }
                                );


            bookDbContext.SaveChanges();
        }
    }
}