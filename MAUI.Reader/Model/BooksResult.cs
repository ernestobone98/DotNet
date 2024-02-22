using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUI.Reader.Model
{
    public class BooksResult
    {
        public int Code { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        
        public List<Book> Books { get; set; }
    }
}
