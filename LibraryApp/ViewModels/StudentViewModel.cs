using LibraryApp.Commands;
using LibraryApp.Helpers;
using LibraryApp.Models;
using LibraryApp.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryApp.ViewModels
{
    public class StudentViewModel:BaseViewModel
    {
		private Student	student;

		public Student Student
		{
			get { return student; }
			set { student = value; OnPropertyChanged(); }
		}


        public StudentViewModel()
        {
            Student = new Student();

            AddStudentCommand = new RelayCommand((obj) =>
            {
                Student.ID = Guid.NewGuid().ToString().Substring(0, 6);
                if (File.Exists(App.StudentFileName))
                {
                    var students = FileHelper.Read<ObservableCollection<Student>>(App.StudentFileName);
                    students.Add(Student);
                    FileHelper.Write(students, App.StudentFileName);
                }
                else
                {
                    var list=new ObservableCollection<Student>();
                    list.Add(Student);
                    FileHelper.Write(list, App.StudentFileName);
                }
                FileHelper.WritePdf(Student);
                Student = new Student();
                MessageBox.Show("Student added successfully");
            }, (pred) =>
            {
                return ValidateHelper.IsValid(Student.Fullname);
            }
            );

            BackCommand = new RelayCommand((obj) =>
            {
                App.MyGrid.Children.Clear();

                var vm = new LibrarianHomeViewModel();
                var view = new LibrarianHomeUC();
                view.DataContext = vm;

                App.MyGrid.Children.Add(view);
            });
        }

        public RelayCommand BackCommand{ get; set; }
        public RelayCommand AddStudentCommand { get; set; }
    }
}
