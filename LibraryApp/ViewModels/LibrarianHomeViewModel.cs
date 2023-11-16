using LibraryApp.Commands;
using LibraryApp.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.ViewModels
{
    public class LibrarianHomeViewModel:BaseViewModel
    {
        public RelayCommand BackCommand { get; set; }
        public RelayCommand ShowAllCommand { get; set; }
        public RelayCommand AddBookCommand { get; set; }

        public LibrarianHomeViewModel()
        {
            AddBookCommand = new RelayCommand((obj) =>
            {
                var vm = new AddBookViewModel();
                var view = new AddBookUC();
                view.DataContext = vm;

                App.MyGrid.Children.Clear();
                App.MyGrid.Children.Add(view);
            });

            BackCommand = new RelayCommand((obj) =>
            {
                var vm=new HomeViewModel();
                var view = new HomeUC();
                view.DataContext = vm;

                App.MyGrid.Children.Clear();
                App.MyGrid.Children.Add(view);
            });

            ShowAllCommand = new RelayCommand((obj) =>
            {
                var vm = new ShowAllBookViewModel();
                var view = new ShowAllBookUC();
                view.DataContext = vm;

                App.MyGrid.Children.Clear();
                App.MyGrid.Children.Add(view);
            });
        }
    }
}
