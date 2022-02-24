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
            foreach (var item in db.Books)
            {
                if (item.NumOfBooks >= Count(item))
                {
                    ComboBook.Items.Add(item.Bookname);
                }
            }
        }
        private int Count(tblBook item)
        {
            int count = 0;
            using (var context = new ARMebContext()) //добавление в бд пользователя
            {
                foreach (var reader in context.Readers)
                {
                    if (reader.BookId == item.Id)
                    {
                        count++;
                    }
                }

                this.Hide();
            }
            
            return count;
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            var group = new Readers()
            {
                Name = txtUsername.Text,
                Age = int.Parse(txtPassword.Text),
                Id = FindBook(sort),
                HaveBooks = Haveb(sort),

            };
            repository.Readers.CreateReader(group);
            this.Hide();
        }
        
        private int FindBook(String bood)
        {
            using (var context = new ARMebContext()) //добавление в бд пользователя
            {
                foreach (var item in context.Books)
                {
                    if (item.Bookname == bood)
                    {
                        return item.Id;
                    }
                }
                return 0;
            }
            
        }
        private bool Haveb(String bood)
        {
            if (bood != "")
            {
                return true;
            }
            else
            {
                return false;
            }
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
