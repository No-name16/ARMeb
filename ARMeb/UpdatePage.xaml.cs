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

namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Window
    {
        tblBook sort;
        ARMebContext db;
        int Id;
        private readonly IRepositoryManager repository;
        public UpdatePage(int userId)
        {
            InitializeComponent();
            db = new ARMebContext();
            db.Readers.Load();
            foreach (var item in db.Books)
            {
                if (item.NumOfBooks >= Count(item))
                {
                    ComboBook.Items.Add(item.Bookname);
                }
            }
            Id = userId;
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
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            var reader = repository.Readers.GetReader(Id, true);
            reader.Name = txtUsername.Text;
            reader.Age = int.Parse(txtPassword.Text);
            reader.TblBooks = sort;
            repository.Readers.UpdateReader(reader);
            this.Hide();
        }

        private void ComboBook_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            sort = (tblBook)selectedItem.Content;

        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ComboBook_Selected_1(object sender, RoutedEventArgs e)
        {
            /*ComboBox comboBox = (ComboBox)sender;
            String selectedItem = (String)comboBox.SelectedItem;
            sort = selectedItem;*/
        }
    }
}
