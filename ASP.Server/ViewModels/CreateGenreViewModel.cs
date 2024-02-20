using ASP.Server.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.Server.ViewModels
{
    public class CreateGenreViewModel
    {

        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un genre
        [Required]
        public string Name { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; init; }

    }
}