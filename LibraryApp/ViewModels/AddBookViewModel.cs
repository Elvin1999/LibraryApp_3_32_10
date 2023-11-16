using LibraryApp.Commands;
using LibraryApp.Helpers;
using LibraryApp.Models;
using LibraryApp.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryApp.ViewModels
{
    public class AddBookViewModel:BaseViewModel
    {
		private Book book;

		public Book Book
        {
			get { return book; }
			set { book = value; OnPropertyChanged(); }
		}

        public RelayCommand AddBookCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public AddBookViewModel()
        {
            Book=new Book();

            AddBookCommand = new RelayCommand((obj) =>
            {
                var books = FileHelper.Read<ObservableCollection<Book>>(App.BookFileName);
                Book.Id = Guid.NewGuid().ToString().Substring(0, 8);
                books.Add(Book);
                FileHelper.Write(books,App.BookFileName);
                MessageBox.Show($"{Book.Title} added successfully");
            });



            BackCommand = new RelayCommand((obj) =>
            {
                App.MyGrid.Children.Clear();

                var vm = new LibrarianHomeViewModel();
                var view = new LibrarianHomeUC();
                view.DataContext = vm;

                App.MyGrid.Children.Add(view);
            });
        }
    }
}
