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
using ARMeb.Contracts;
using System.Linq;
using ARMeb.Repository;

namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Window
    {
        String sort;
        int Id;
        string bookid = "";
        static ARMebContext db = new ARMebContext();
        RepositoryManager repository = new RepositoryManager(db);
        public UpdatePage(int userId)
        {
            var readers = repository.Readers.GetReader(userId,true);
            InitializeComponent();
            
            foreach (var item in repository.Books.GetAllBooks(true))
            {
                if (item.IsAny == true)
                {
                    ComboBook.Items.Add(item.Id+". "+item.Bookname);
                }
            }
            txtUsername.Text = readers.Name;
            txtPassword.Text = readers.Age.ToString();
            if (readers.TblBooks != null)
            {
                bookid = readers.BookId.ToString();
            }
            Id = userId;
        }
        

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            var reader = repository.Readers.GetReader(Id, true);
            reader.Name = txtUsername.Text;
            reader.Age = int.Parse(txtPassword.Text);
            if (reader.TblBooks != null)
            {
                var newbok = reader.TblBooks;
                newbok.IsAny = true;
                repository.Books.Update(newbok);

            }
            if (sort != null)
            {
                foreach (char st in sort)
                {
                    if (st == '.')
                    {
                        break;
                    }
                    else
                    {
                        bookid += st;
                    }
                }
                if (repository.Books.GetBook(reader.BookId, true).NumOfBooks <= repository.Books.GetBook(reader.BookId, true).Readers.Count()+1)
                {
                    
                    var uppbook = repository.Books.GetBook(int.Parse(bookid), true);
                    uppbook.IsAny = false;
                    repository.Books.UpdateBook(uppbook);
                }
            } else
            {
                reader.HaveBooks = false;
            }
            repository.Readers.UpdateReader(reader);
            
            this.Hide();
        }

        
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ComboBook_Selected_1(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            String selectedItem = (String)comboBox.SelectedItem;
            sort = selectedItem;
        }
    }
}
