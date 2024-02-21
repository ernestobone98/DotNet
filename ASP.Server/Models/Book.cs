﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ASP.Server.Controllers;


namespace ASP.Server.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        // Mettez ici les propriété de votre livre: Nom, Autheur, Prix, Contenu et Genres associés
        // N'oublier pas qu'un livre peut avoir plusieur genres

        public string Name { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public string Content { get; set; }
        [Required] 
        public List<Genre> Genres { get; set; }

        // public Book()
        // {
        //     Genres = new List<Genre>();
        // }

    }
}