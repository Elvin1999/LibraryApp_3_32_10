using LibraryApp.Commands;
using LibraryApp.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.ViewModels
{
    public class HomeViewModel:BaseViewModel
    {
        public RelayCommand SelectLibrarianCommand { get; set; }
        public HomeViewModel()
        {
            SelectLibrarianCommand = new RelayCommand((obj) =>
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
