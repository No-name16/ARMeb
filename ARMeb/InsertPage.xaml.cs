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
using System.Text.RegularExpressions;
using ARMeb.Models;
using System.Data.Entity;
using System.Linq;
using ARMeb.Contracts;

namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для InsertPage.xaml
    /// </summary>
    public partial class InsertPage : Window
    {
        ARMebContext db;
        String sort;
        private  IRepositoryManager repository;
        public InsertPage()
        {
            InitializeComponent();
            db = new ARMebContext();
            db.Books.Load();
            db.Readers.Load();
            foreach (var item in repository.Books.GetAllBooks(true))
            {
                if (item.IsAny == true)
                {
                    ComboBook.Items.Add(item.Bookname);
                }
            }
        }
        
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Readers reader = new Readers();
            reader.Name = txtUsername.Text;
            reader.Age = int.Parse(txtPassword.Text);
            reader.BookId = repository.Books.GetId(sort, true);
            reader.TblBooks = repository.Books.GetBook(reader.BookId, true);
            repository.Readers.CreateReader(reader);
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

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            sort = selectedItem.Content.ToString();

        }

        private void ComboBook_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            String selectedItem = (String)comboBox.SelectedItem;
            sort = selectedItem;
        }
    }
}
