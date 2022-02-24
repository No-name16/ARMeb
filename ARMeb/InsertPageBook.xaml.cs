﻿using System;
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

namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для InsertPageBook.xaml
    /// </summary>
    public partial class InsertPageBook : Window
    {
        ARMebContext db;
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
                context.Books.Add(group);
                context.SaveChanges();

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
    
