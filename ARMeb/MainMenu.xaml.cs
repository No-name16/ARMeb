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

namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        ARMebContext db;

        public MainMenu()
        {
            InitializeComponent();
            db = new ARMebContext();
            db.Listoperations.Load(); // загружаем данные
            operationsGrid.ItemsSource = db.Listoperations.Local.ToBindingList();
        }
        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void opnBookWView_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
