using ASP.Server.Models;

namespace ASP.Server.ViewModels
{
    public class StatsViewModel
    {
        public float AveragePrice { get; set; }
        public int TotalBooks { get; set; }
        public int TotalAuthors { get; set; }
        public int TotalGenres { get; set; }
        public double AverageContentLength { get; set; }
    }
}