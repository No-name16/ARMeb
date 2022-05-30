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
using ARMeb.Repository;

namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        static ARMebContext db = new ARMebContext();
        RepositoryManager repository = new RepositoryManager(db);

        public MainMenu()
        {
            InitializeComponent();
            db = new ARMebContext();
            db.Listoperations.Load(); // загружаем данные
            operationsGrid.ItemsSource = db.Listoperations.Local.ToBindingList();
        }
        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            foreach (Operations item in repository.Operations.GetAllOperations(true))
            {
                repository.Operations.DeleteOperation(item);
                repository.Save();
            }

            Application.Current.Shutdown();

        }

        private void opnBookWView_Click(object sender, RoutedEventArgs e)
        {
            Search window = new Search();
            window.Show();
            this.Close();
        }

        private void opnReaderView_Click(object sender, RoutedEventArgs e)
        {
            Users window = new Users();
            window.Show();
            this.Close();
        }

        private void opnBorrow_Click(object sender, RoutedEventArgs e)
        {
            AddDel window = new AddDel();
            window.Show();
            this.Close();
        }

        private void opn_Click(object sender, RoutedEventArgs e)
        {
            AddDelBooks window = new AddDelBooks();
            window.Show();
            this.Close();
        }
    }
}
