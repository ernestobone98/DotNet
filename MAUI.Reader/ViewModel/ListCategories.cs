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
    public partial class ListCategories : INotifyPropertyChanged
    {
        private LibraryService libraryService;

        public ListCategories()
        {
            this.libraryService = Ioc.Default.GetService<LibraryService>();
            ItemSelectedCommand = new RelayCommand<Genre>(OnItemSelected);
            GetGenresCommand.Execute(null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ItemSelectedCommand { get; private set; }

        public ObservableCollection<Genre> Genres { get; private set; } = new ObservableCollection<Genre>()
            { new Genre() { Name = "test" } };

        public Genre SelectedGenre = new();
        
        [RelayCommand]
        async Task GetGenres()
        {
            var genres = await libraryService.GetGenresAsync();
            //Genres.Clear();
            // Workaround
            Genres = new ObservableCollection<Genre>(genres);
            
            //foreach (var genre in genres)
            //{
            //    Genres.Add(genre);
            //}
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Genres)));
        }

        // Méthode appelée lorsque l'utilisateur sélectionne un livre
        private void OnItemSelected(Genre selectedGenre)
        {
            
        }
        
    }
}