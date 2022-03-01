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
using System.Text.RegularExpressions;
using ARMeb.Repository;

namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для UpdatePageBook.xaml
    /// </summary>
    public partial class UpdatePageBook : Window
    {
        static ARMebContext db = new ARMebContext();
        RepositoryManager repository = new RepositoryManager(db);
        int Id;
        public UpdatePageBook(int userId)
        {
            InitializeComponent();
            db = new ARMebContext();
            db.Readers.Load();
           
            Id = userId;
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            tblBook item = new tblBook();
            item.Bookname = txtUsername.Text;
            item.BookAuthor = txtAuthor.Text;
            item.NumOfBooks = int.Parse(txtPassword.Text);
            repository.Books.UpdateBook(item);
            this.Hide();
        }
       
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
