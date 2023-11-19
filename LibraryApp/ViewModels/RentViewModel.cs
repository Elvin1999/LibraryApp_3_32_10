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
    public class RentViewModel : BaseViewModel
    {

        #region Book


        private string searchBookText;

        public string SearchBookText
        {
            get { return searchBookText; }
            set { searchBookText = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Book> allBooks;

        public ObservableCollection<Book> AllBooks
        {
            get { return allBooks; }
            set { allBooks = value; OnPropertyChanged(); }
        }

        private Book selectedBook;

        public Book SelectedBook
        {
            get { return selectedBook; }
            set { selectedBook = value; OnPropertyChanged(); }
        }


        public ObservableCollection<Book> Books { get; set; }
        public RelayCommand SearchBookCommand { get; set; }

        #endregion



        public ObservableCollection<Student> Students { get; set; }
        public RelayCommand SearchStudentCommand { get; set; }

        private string searchStudentText;

        public string SearchStudentText
        {
            get { return searchStudentText; }
            set { searchStudentText = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Student> allStudents;

        public ObservableCollection<Student> AllStudents
        {
            get { return allStudents; }
            set { allStudents = value; OnPropertyChanged(); }
        }

        private Student selectedStudent;

        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set { selectedStudent = value; OnPropertyChanged(); }
        }

        private double totalPrice;

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; OnPropertyChanged(); }
        }

        private int selectedDay;

        public int SelectedDay
        {
            get { return selectedDay; }
            set
            {
                selectedDay = value; OnPropertyChanged();
                if (SelectedBook != null && SelectedStudent != null)
                {
                    TotalPrice = SelectedBook.Price * SelectedDay;
                }
            }
        }


        public RelayCommand BackCommand { get; set; }
        public RelayCommand SubmitCommand { get; set; }

        public RentViewModel()
        {

            SubmitCommand = new RelayCommand((obj) =>
            {
                if (SelectedBook != null && SelectedStudent != null && SelectedDay >= 1)
                {
                    var rent = new Rent
                    {
                        Book = SelectedBook,
                        Student = SelectedStudent,
                        Days = SelectedDay,
                        TakeOut = DateTime.Now
                    };

                    if (File.Exists(App.RentFileName))
                    {
                        var rents = FileHelper.Read<ObservableCollection<Rent>>(App.RentFileName);
                        rents.Add(rent);
                        FileHelper.Write(rents, App.RentFileName);
                    }
                    else
                    {
                        var list = new ObservableCollection<Rent>();
                        list.Add(rent);
                        FileHelper.Write(list, App.RentFileName);
                    }
                }
            });
            BackCommand = new RelayCommand((obj) =>
            {
                App.MyGrid.Children.Clear();

                var vm = new LibrarianHomeViewModel();
                var view = new LibrarianHomeUC();
                view.DataContext = vm;

                App.MyGrid.Children.Add(view);
            });

            Books = FileHelper.Read<ObservableCollection<Book>>(App.BookFileName);
            AllBooks = new ObservableCollection<Book>(Books);
            SearchBookCommand = new RelayCommand((obj) =>
            {
                var result = Books.Where(b => b.Title.ToLower().Contains(SearchBookText.Trim().ToLower()) ||
                SearchBookText.Trim() == ""
                ).ToList();
                AllBooks = new ObservableCollection<Book>(result);

                if (SelectedBook != null && SelectedStudent != null)
                {
                    TotalPrice = SelectedBook.Price * SelectedDay;
                }

            });


            Students = FileHelper.Read<ObservableCollection<Student>>(App.StudentFileName);
            AllStudents = new ObservableCollection<Student>(Students);
            SearchStudentCommand = new RelayCommand((obj) =>
            {
                var result = Students.Where(b => b.Fullname.ToLower().Contains(SearchStudentText.Trim().ToLower()) ||
                SearchStudentText.Trim() == "" || b.ID.ToLower().Contains(SearchStudentText.Trim().ToLower())
                ).ToList();
                AllStudents = new ObservableCollection<Student>(result);
            });
        }
    }
}
