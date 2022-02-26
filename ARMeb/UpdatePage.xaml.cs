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

namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Window
    {
        String sort;
        ARMebContext db;
        int Id;
        private IRepositoryManager repository;
        public UpdatePage(int userId)
        {
            var readers = repository.Readers.GetReader(userId,true);
            InitializeComponent();
            db = new ARMebContext();
            db.Readers.Load();
            
            foreach (var item in repository.Books.GetAllBooks(true))
            {
                if (item.IsAny == true)
                {
                    ComboBook.Items.Add(item.Bookname);
                }
            }
            txtUsername.Text = readers.Name;
            txtPassword.Text = readers.Age.ToString();
            sort = readers.TblBooks.Bookname;
            Id = userId;
        }
        

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            var reader = repository.Readers.GetReader(Id, true);
            reader.Name = txtUsername.Text;
            reader.Age = int.Parse(txtPassword.Text);
            reader.BookId = repository.Books.GetId(sort, true);
            reader.TblBooks = repository.Books.GetBook(reader.BookId,true);
            repository.Readers.UpdateReader(reader);
            foreach (var item in repository.Books.GetAllBooks(true))
            {
                var readerlist = db.Readers.Where(x => x.BookId == repository.Books.GetId(item.Bookname, true)).ToList();
                if (item.NumOfBooks > readerlist.Count())
                {
                    var uppbook = repository.Books.GetBook(item.Id, true);
                    uppbook.IsAny = false;
                    repository.Books.UpdateBook(uppbook);
                }
            }
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
