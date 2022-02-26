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

namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для AddDel.xaml
    /// </summary>
    public partial class AddDel : Window
    {
        static ARMebContext db;
        private readonly IRepositoryManager repository;
        Repository.ReaderRepository reader1 = new Repository.ReaderRepository(db);
        public AddDel()
        {
            InitializeComponent();
            db = new ARMebContext();
            List<Re> data = new List<Re>();

            var readers = reader1.GetAllReaders(trackChanges: false);

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
            myDataGrid.ItemsSource = data;
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            InsertPage page = new InsertPage();
            page.ShowDialog();
            var readers = repository.Readers.GetAllReaders(trackChanges: false);
            List<Re> data = new List<Re>();
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
            myDataGrid.ItemsSource = data;
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ARMebContext()) //добавление в бд пользователя
            {
                foreach (var item in db.Readers)
                {

                    if (item.Id.Equals(((Readers)myDataGrid.SelectedItem).Id))
                    {
                        UpdatePage page = new UpdatePage(item.Id);
                        page.ShowDialog();
                        var readers = repository.Readers.GetAllReaders(trackChanges: false);
                        List<Re> data = new List<Re>();
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
                        myDataGrid.ItemsSource = data;

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
            var newper = repository.Readers.GetReader(((Readers)myDataGrid.SelectedItem).Id, false);
            repository.Readers.DeleteReader(newper);
            List<Re> data = new List<Re>();
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
            myDataGrid.ItemsSource = data;
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