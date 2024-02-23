using ASP.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace ASP.Server.Dtos
{ 
    public class BookPartialDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public List<AuthorPartialDTO> Authors { get; set; }
        public List<GenreDTO> Genres { get; set; }
    }
}