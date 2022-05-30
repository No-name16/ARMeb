using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ARMeb.Models;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using ARMeb.Repository;

namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для InsertPageBook.xaml
    /// </summary>
    public partial class InsertPageBook : Window
    {
        static ARMebContext db = new ARMebContext();
        RepositoryManager repository = new RepositoryManager(db);
        String sort;
        public InsertPageBook()
        {
            InitializeComponent();
            db = new ARMebContext();
            db.Books.Load();
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ARMebContext()) //добавление в бд пользователя
            {
                var group = new tblBook()
                {
                    Bookname = txtUsername.Text,
                    BookAuthor = txtAuthor.Text,
                    NumOfBooks = int.Parse(txtPassword.Text),
                    IsAny = Haveb(int.Parse(txtPassword.Text)),
                };
                repository.Books.CreateBook(group);
                context.SaveChanges();
                Operations operation = new Operations();
                operation.Time = DateTime.Now;
                operation.Title = "Созднаие новой книги: " + group.Bookname+" -  "+ group.BookAuthor;
                repository.Operations.CreateOperation(operation);
                this.Hide();
            }
        }
        
        private bool Haveb(int num)
        {
            if (num != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       
    }
}
    
