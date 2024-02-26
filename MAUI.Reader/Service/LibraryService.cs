using System.Collections.ObjectModel;
using System.Net.Http.Json;
using MAUI.Reader.Model;

namespace MAUI.Reader.Service
{
    public class LibraryService
    {
        private const string BASE_URL = "https://localhost:5001/api";

        private HttpClient client;
        // A remplacer avec vos propre données !!!!!!!!!!!!!!
        // Pensé qu'il ne faut mieux ne pas réaffecter la variable Books, mais juste lui ajouter et / ou enlever des éléments
        // Donc pas de LibraryService.Instance.Books = ...
        // mais plutot LibraryService.Instance.Books.Add(...)
        // ou LibraryService.Instance.Books.Clear()
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>() {
            new Book(),
            new Book(),
            new Book()
        };

        // C'est aussi ici que vous ajouterez les requête réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
        // Faite bien attention a ce que votre requête réseau ne bloque pas l'interface 
        public LibraryService()
        {
            HttpClientHandler h = new HttpClientHandler();
            h.ClientCertificateOptions = ClientCertificateOption.Manual;
            h.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            client = new HttpClient(h);
        }
        
        public async Task<List<Book>> GetBooksAsync()
        {
            List<Book> books = new();
            var url = $"{BASE_URL}/book?offset={0}&limit={10}";
            try
            {
                var response = await client.GetAsync(url);


                if (response.IsSuccessStatusCode)
                {
                    books = await response.Content.ReadFromJsonAsync<List<Book>>();
                }

                return books;
            }
            catch (Exception e)
            {
                return [];
            }
        }
        
        public async Task<List<Genre>> GetGenresAsync()
        {
            List<Genre> genres = new();
            var url = $"{BASE_URL}/Genre";
            try
            {
                var response = await client.GetAsync(url);


                if (response.IsSuccessStatusCode)
                {
                    genres = await response.Content.ReadFromJsonAsync<List<Genre>>();
                }

                return genres;
            }
            catch (Exception e)
            {
                return [];
            }
        }
    }
}
