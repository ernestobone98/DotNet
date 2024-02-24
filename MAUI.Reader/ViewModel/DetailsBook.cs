using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;
using MAUI.Reader.Model;

namespace MAUI.Reader.ViewModel
{
    // Make sure to remove the ';' after the namespace declaration
    [QueryProperty("Book", "Book")]
    public partial class DetailsBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Une commande permet de recevoir des évènement de l'IHM
        public ICommand ReadBook2Command { get; init; } = new RelayCommand<Book>(x => { /* A vous de définir la commande */ });

        // You can also use this form to define a command. The line above does exactly the same thing; choose one of the 2 forms
        [RelayCommand]
        public void ReadBook(Book book)
        {
            /* A vous de définir la commande */
        }

        // n'oublier pas faire de faire le binding dans DetailsBook.xaml !!!!
        public Book SelectedBook { get; set; }

        // Constructor to accept Book parameter and initialize SelectedBook
        public DetailsBook(Book book)
        {
            SelectedBook = book;
        }
    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(new Book() /*{ Title = "Test Book" }*/) { }
    }
}