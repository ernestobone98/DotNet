using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MAUI.Reader.Model;
using MAUI.Reader.Service;
using System.Windows.Input;

using CommunityToolkit.Maui.Alerts;

namespace MAUI.Reader.ViewModel
{
    public partial class ListBooks : INotifyPropertyChanged
    {
        private LibraryService libraryService;
        
        public ListBooks()
        {
            this.libraryService = Ioc.Default.GetService<LibraryService>();
            ItemSelectedCommand = new RelayCommand<Book>(OnItemSelected);
            //LoadBooks(); // Chargement des livres au démarrage de la page
            GetBooksCommand.Execute(null);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        
        public ICommand ItemSelectedCommand { get; private set; }
        
        // Liste des livres
        public ObservableCollection<Book> Books { get; private set; } = new ObservableCollection<Book>() {new Book(){Name = "test"}};

        // Méthode pour charger les livres (peut être remplacée par une méthode asynchrone pour récupérer les livres depuis un service)
        /*private void LoadBooks()
        {
            // Exemple de chargement de livres (à remplacer par votre propre logique de chargement)
            Books.Add(new Book { Name = "Livre 1", Author = "Auteur 1", Price = "$10", Content = "Contenu du livre 1", Genres = "Fiction" });
            Books.Add(new Book { Name = "Livre 2", Author = "Auteur 2", Price = "$15", Content = "Contenu du livre 2", Genres = "Non-fiction" });
            Books.Add(new Book { Name = "Livre 3", Author = "Auteur 3", Price = "$20", Content = "Contenu du livre 3", Genres = "Mystère" });
        }*/

        [RelayCommand]
        async Task GetBooks()
        {
           var books= await libraryService.GetBooksAsync();
            Books.Clear();
            foreach (var book in books)
            {
                try
                {
                    Books.Add(book);
                }
                catch (Exception e)
                {
                    return;
                }
            }
        }
        
        // Méthode appelée lorsque l'utilisateur sélectionne un livre
        private void OnItemSelected(Book selectedBook)
        {
            // Vous pouvez ajouter ici la logique pour naviguer vers les détails du livre sélectionné
            // Par exemple :
            // Ioc.Default.GetRequiredService<INavigationService>().NavigateToDetails(selectedBook);
        }
        
    }
}
