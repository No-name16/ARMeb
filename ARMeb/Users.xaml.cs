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
using System.Linq;
using ARMeb.Contracts;
using ARMeb.Repository;
using System.Text.RegularExpressions;

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
        static ARMebContext db = new ARMebContext();
        RepositoryManager repository = new RepositoryManager(db);
        public Users()
        {
            InitializeComponent();
            // загружаем данны
        }
        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            MainMenu window = new MainMenu();
            window.Show();
            this.Close();
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void opnBookWView_Click(object sender, RoutedEventArgs e)
        {
            // Извлечь всех заказчиков и отобразить их имена в консоли
            IEnumerable<Readers> list;
            string name = BookName.Text;
            int age;
            if (BookAuthor.Text == "")
            {
                age = 0;
            }
            else
            {
                age = int.Parse(BookAuthor.Text);
            }
            var readers = repository.Readers.GetAllReaders(trackChanges: false);
            IEnumerable<Readers> readerlist;
            if (name == "" && age == 0)
            {

                bookGrid.ItemsSource = readers;
            }
            else if (name != "" && age != 0)
            {
                readerlist = readers.Where(x => x.Name == name && x.Age == age);
                bookGrid.ItemsSource = readerlist;
            } else {

                readerlist = readers.Where(x => x.Name == name || x.Age == age);
                bookGrid.ItemsSource = readerlist;
            }
                    
            
        }
        
    }

}
