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
            ItemSelectedCommand = new RelayCommand<Book>(async (book) => await GoToDetailsAsync(book));
            //LoadBooks(); // Chargement des livres au démarrage de la page
            GetBooksCommand.Execute(null);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public ICommand ItemSelectedCommand { get; private set; }
        
        // Liste des livres
        public ObservableCollection<Book> Books { get; private set; } = new ObservableCollection<Book>() {new Book(){Name = "test"}};

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

        [RelayCommand]
        async Task GoToDetailsAsync(Book book)
        {
            var navigationService = Ioc.Default.GetRequiredService<NavigationService>();
            await navigationService.NavigateToDetails(book);
        }
        
        // Méthode appelée lorsque l'utilisateur sélectionne un livre
        // Call GoToDetailsAsync when a book is selected
        private void OnItemSelected(Book selectedBook)
        {
            GoToDetailsAsync(selectedBook);
        }
        
        // Méthode appelée lorsque l'utilisateur sélectionne un livre
        // private void OnItemSelected(Book selectedBook)
        // {
        //     // Vous pouvez ajouter ici la logique pour naviguer vers les détails du livre sélectionné
        //     // Par exemple :
        //     // Ioc.Default.GetRequiredService<INavigationService>().NavigateToDetails(selectedBook);
        // }
        
    }
}
