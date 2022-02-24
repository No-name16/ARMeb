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
using ARMeb.Contracts;

namespace ARMeb
{
    public class Re
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bookname { get; set; }
        public int Age { get; set; }
        public bool HaveBooks { get; set; }
        
    }

    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        String sort;
        ARMebContext db;
        private readonly IRepositoryManager repository;
        public Users()
        {
            InitializeComponent();
            db = new ARMebContext();
            db.Readers.Load();
            // загружаем данны
        }
        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            MainMenu window = new MainMenu();
            window.Show();
            this.Close();
        }

        private void opnBookWView_Click(object sender, RoutedEventArgs e)
        {
            // Извлечь всех заказчиков и отобразить их имена в консоли
            List<Re> data = new List<Re>();
            string name = BookName.Text;
            int age = int.Parse(BookAuthor.Text);
            var readers = repository.Readers.GetAllReaders(trackChanges: true);
            
            foreach (Readers reader in readers)
            {
                Re newperson = new Re()
                {
                    Id = reader.Id,
                    Name = reader.Name,
                    HaveBooks = reader.HaveBooks,
                    Bookname = reader.TblBooks.Bookname,
                    Age = reader.Age,
                };
                data.Add(newperson);
            }

            bookGrid.ItemsSource = data;
        }  
        private void sortType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            TextBlock selectedItem = (TextBlock)comboBox.SelectedItem;
            sort = selectedItem.Text.ToString();
        }
    }
}