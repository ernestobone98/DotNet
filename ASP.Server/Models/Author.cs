using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.Server.Models;

public class Author
{
    [Required] public string Name { get; set; }
    public IEnumerable<Book> Book { get; set; }
}