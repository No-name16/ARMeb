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
using ARMeb.Repository;

namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для InsertPage.xaml
    /// </summary>
    public partial class InsertPage : Window
    {
        
        String sort;
        static ARMebContext db = new ARMebContext();
        RepositoryManager repository = new RepositoryManager(db);
        string bookid = "";
        public InsertPage()
        {
            InitializeComponent();
            
            foreach (var item in repository.Books.GetAllBooks(true))
            {
                if (item.IsAny == true)
                {
                    ComboBook.Items.Add(item.Id+". "+item.Bookname);
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
                if (repository.Books.GetBook(int.Parse(bookid),true) != null)
                {
                    if (repository.Books.GetBook(int.Parse(bookid), true).NumOfBooks <= repository.Books.GetBook(int.Parse(bookid), true).HandBookValue + 1)
                    {

                        var uppbook = repository.Books.GetBook(int.Parse(bookid), true);
                        uppbook.IsAny = false;
                        repository.Books.UpdateBook(uppbook);
                    }
                }
                var book = repository.Books.GetBook(int.Parse(bookid), true);
                reader.TblBooks = book;
                book.HandBookValue += 1;
                repository.Books.UpdateBook(book);
                reader.HaveBooks = true;
                repository.Readers.CreateReader(reader);
                Operations operation = new Operations();
                operation.Time = DateTime.Now;
                operation.Title = "Созднаие нового читателя " + reader.Name;
                repository.Operations.CreateOperation(operation);
                this.Hide();
            } else
            {
                reader.TblBooks = null;
                reader.HaveBooks = false;
                repository.Readers.CreateReader(reader);
                Operations operation = new Operations();
                operation.Time = DateTime.Now;
                operation.Title = "Созднаие нового читателя " + reader.Name;
                repository.Operations.CreateOperation(operation);
                this.Hide();
            }
        }

        private void ComboBook_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            String selectedItem = (String)comboBox.SelectedItem;
            sort = selectedItem;
        }
    }
}
