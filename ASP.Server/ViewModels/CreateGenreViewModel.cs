using ASP.Server.Controllers;
using ASP.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.Server.ViewModels
{
    public class CreateGenreViewModel
    {
        [Required]
        public int Id { get; internal set; }

        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un genre
        [Required]
        public string Name { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; init; }

        public IEnumerable<int> Books { get; set; }

        // Liste de livre a afficher à l'utilisateur
        public IEnumerable<Book> AllBooks { get; set; }

        public string SearchTerm { get; set; }



    }
}