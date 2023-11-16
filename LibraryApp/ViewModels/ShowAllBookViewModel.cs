using LibraryApp.Commands;
using LibraryApp.Helpers;
using LibraryApp.Models;
using LibraryApp.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.ViewModels
{
    public class ShowAllBookViewModel:BaseViewModel
    {

        private ObservableCollection<Book> allBooks;

		public ObservableCollection<Book> AllBooks
		{
			get { return allBooks; }
			set { allBooks = value; }
		}


        public RelayCommand BackCommand { get; set; }


        public ShowAllBookViewModel()
        {
            if (!File.Exists(App.BookFileName))
            {
                AllBooks = new ObservableCollection<Book>
                {
                    new Book
                    {
                        Id=Guid.NewGuid().ToString().Substring(0,8),
                         Title="Crime and Punishment",
                          Discount=10,
                           BookCount=20,
                            Page=527,
                             Price=17.75
                    }
                };
                FileHelper.Write(AllBooks, App.BookFileName);
            }
            else
            {
                AllBooks = FileHelper.Read<ObservableCollection<Book>>(App.BookFileName);
            }


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
