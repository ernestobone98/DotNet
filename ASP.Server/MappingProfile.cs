﻿using ASP.Server.Database;
using ASP.Server.Dtos;
using ASP.Server.Models;
using ASP.Server.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace ASP.Server
{
    public class MappingProfile : Profile
    { 
        public MappingProfile()
        {
            // Rajouter autant de ligne ici que vous avez de mapping Model <-> DTO
            // https://docs.automapper.org/en/latest/
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors));
            CreateMap<Book, BookPartialDTO>();
            CreateMap<Genre, GenreDTO>();
            CreateMap<Genre, GenrePartialDTO>();
            CreateMap<Author, AuthorPartialDTO>();
        }
    }
}
