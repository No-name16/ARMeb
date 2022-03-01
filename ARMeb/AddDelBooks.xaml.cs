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
using ARMeb.Repository;

namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для AddDelBooks.xaml
    /// </summary>
    public partial class AddDelBooks : Window
    {
        static ARMebContext db = new ARMebContext();
        RepositoryManager repository = new RepositoryManager(db);
        public AddDelBooks()
        {
            InitializeComponent();
            db.Books.Load();
            myDataGrid.ItemsSource = db.Books.Local.ToBindingList();
        }
        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            InsertPageBook page = new InsertPageBook();
            page.ShowDialog();
            myDataGrid.ItemsSource = db.Books.Local.ToBindingList();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ARMebContext()) //добавление в бд пользователя
            {
                foreach (var item in db.Books)
                {

                    if (item.Id.Equals(((tblBook)myDataGrid.SelectedItem).Id))
                    {
                        UpdatePageBook page = new UpdatePageBook(item.Id);
                        page.ShowDialog();
                        myDataGrid.ItemsSource = db.Books.Local.ToBindingList();

                    }
                }
            }

        }
        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            MainMenu window = new MainMenu();
            window.Show();
            this.Close();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var newper = repository.Books.GetBook(((tblBook)myDataGrid.SelectedItem).Id, true);
            if (newper.Readers != null)
            {
                MessageBox.Show("Книга на руках у читателя, не может быть удалена");
            }
            else
            {
                repository.Books.DeleteBook(newper);
            }
            var books = repository.Books.GetAllBooks(trackChanges: true);
            myDataGrid.ItemsSource = books;

        }
    }
}