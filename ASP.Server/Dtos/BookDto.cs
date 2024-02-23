using ASP.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace ASP.Server.Dtos
{ 
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Content { get; set; }
        public List<AuthorPartialDTO> Authors { get; set; }
        public List<GenrePartialDTO> Genres { get; set; }
    }
}
