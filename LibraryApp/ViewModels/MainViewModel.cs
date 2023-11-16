using LibraryApp.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibraryApp.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        public MainViewModel()
        {
            var homeViewModel=new HomeViewModel();
            var homeUC = new HomeUC();
            homeUC.DataContext= homeViewModel;

            App.MyGrid.Children.Add(homeUC);
        }
    }
}
