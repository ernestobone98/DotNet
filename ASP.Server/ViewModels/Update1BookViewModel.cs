using ASP.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.Server.ViewModels
{
    public class Update1BookViewModel
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        // Ajouter ici tous les champs que l'utilisateur devra remplir pour mettre à jour un livre

        // Liste des genres sélectionnés par l'utilisateur
        [Required(ErrorMessage = "You need at least 1 genre"), MinLength(1)]
        public List<int> Genres { get; set; }

        public IEnumerable<Genre> AllGenres { get; set; }

        [Required]
        public string Authors { get; set; }
        
        public string Content { get; set; }
        
        public float Price { get; set; }
    }
}