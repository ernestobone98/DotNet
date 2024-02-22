using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.Server.Database;
using ASP.Server.Models;
using AutoMapper;
using ASP.Server.Dtos;
using AutoMapper.QueryableExtensions;

namespace ASP.Server.Api
{
    [Route("/api/book")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly IMapper _mapper;

        public BooksController(LibraryDbContext libraryDbContext, IMapper mapper)
        {
            _libraryDbContext = libraryDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookPartialDTO>> GetBooks([FromQuery] int offset = 0, [FromQuery] int limit = 10, [FromQuery] int? genre = null)
        {
            IQueryable<Book> query = _libraryDbContext.Books.Include(b => b.Author).Include(b => b.Genres);

            if (genre.HasValue)
            {
                query = query.Where(b => b.Genres.Any(g => g.Id == genre));
            }

            int totalCount = query.Count();
            var books = query.Skip(offset).Take(limit).ToList();

            Response.Headers.Add("Pagination", $"{offset + 1}-{offset + books.Count}/{totalCount}");

            return _mapper.Map<List<BookPartialDTO>>(books);
        }
    }
}