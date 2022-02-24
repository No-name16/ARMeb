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

namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для UpdatePageBook.xaml
    /// </summary>
    public partial class UpdatePageBook : Window
    {
        tblBook sort;
        ARMebContext db;
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

            using (var context = new ARMebContext()) //добавление в бд пользователя
            {
                foreach (var item in db.Books)
                {
                    if (item.Id.Equals(Id))
                    {

                        item.Bookname = txtUsername.Text;
                        item.BookAuthor = txtAuthor.Text;
                        item.NumOfBooks = int.Parse(txtPassword.Text);
                        context.Books.Add(item);
                        context.Books.Attach(item);
                        context.Entry(item).State = EntityState.Modified;
                        context.SaveChanges();
                        this.Hide();
                    }
                }
            }


        }
       
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
