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

namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для AddDel.xaml
    /// </summary>
    public partial class AddDel : Window
    {
        static ARMebContext db = new ARMebContext();
        RepositoryManager repository = new RepositoryManager(db);

        public AddDel()
        {
            InitializeComponent();
            db = new ARMebContext();
            var readers = repository.Readers.GetAllReaders(trackChanges: true);
            DataShow(readers);
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            InsertPage page = new InsertPage();
            page.ShowDialog();
            var readers = repository.Readers.GetAllReaders(trackChanges: true);
            DataShow(readers);
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {

            UpdatePage page = new UpdatePage(((Re)myDataGrid.SelectedItem).Id);
            page.ShowDialog();
            var readers = repository.Readers.GetAllReaders(trackChanges: true);
            DataShow(readers);


        }
        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            MainMenu window = new MainMenu();
            window.Show();
            this.Close();
        }

        private void DataShow(IEnumerable<Readers> readers)
        {
            List<Re> data = new List<Re>();
            foreach (Readers reader in readers)
            {
                string book = "";
                if (reader.TblBooks == null)
                {
                    book = "Нету книг";
                }
                else
                {
                    book = reader.TblBooks.Bookname;
                }
                Re newperson = new Re()
                {
                    Id = reader.Id,
                    Name = reader.Name,
                    HaveBooks = reader.HaveBooks,
                    Bookname = book,
                    Age = reader.Age,
                };

                data.Add(newperson);
            }
            myDataGrid.ItemsSource = data;
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var newper = repository.Readers.GetReader(((Re)myDataGrid.SelectedItem).Id, true);
            if (newper.TblBooks != null)
            {
                var readerlist = db.Readers.Where(x => x.BookId == newper.BookId).ToList();
                if (newper.TblBooks.NumOfBooks < readerlist.Count()-1 )
                {
                    var uppbook = repository.Books.GetBook(newper.BookId, true);
                    uppbook.IsAny = true;
                    repository.Books.UpdateBook(uppbook);
                }
            }
            repository.Readers.DeleteReader(newper);
            var readers = repository.Readers.GetAllReaders(trackChanges: true);
            DataShow(readers);
        }
    }

    /*using (var context = new ARMebContext()) //добавление в бд пользователя
    {
        var removeable =  context.Readers.Where(c => c.Id == ((Readers)myDataGrid.SelectedItem).Id).FirstOrDefault();
        context.Readers.Remove(removeable);
        context.SaveChanges();
        myDataGrid.ItemsSource = db.Readers.Local.ToBindingList();

    }*/


}