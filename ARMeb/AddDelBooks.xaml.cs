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


namespace ARMeb
{
    /// <summary>
    /// Логика взаимодействия для AddDelBooks.xaml
    /// </summary>
    public partial class AddDelBooks : Window
    {
        ARMebContext db;
        public AddDelBooks()
        {
            InitializeComponent();
            db = new ARMebContext();
            db.Books.Load();
            foreach(tblBook item in db.Books)
            {
                int co = Count(item);
                if (co == item.NumOfBooks)
                {
                    using (var context = new ARMebContext()) //добавление в бд пользователя
                    {
                        foreach(tblBook book in context.Books)
                        {
                            if (item == book)
                            {
                                book.IsAny = false;
                                context.Books.Add(book);
                                context.Books.Attach(book);
                                context.Entry(book).State = EntityState.Modified;
                                context.SaveChanges();
                                this.Hide();
                            }
                        }

                        
                            
                    }
                }
            }
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
        private int Count(tblBook item)
        {
            int count = 0;
            using (var context = new ARMebContext()) //добавление в бд пользователя
            {
                foreach (var reader in context.Readers)
                {
                    if (reader.TblBooks == item)
                    {
                        count++;
                    }
                }

                return count;
            }

        }
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ARMebContext()) //добавление в бд пользователя
            {
                foreach (var item in db.Books)
                {
                    Label newlabl = new Label();
                    newlabl.Content = item.Id.ToString();
                    if (item.Id.Equals(((tblBook)myDataGrid.SelectedItem).Id))
                    {
                        if (Count(item) == 0)
                        {
                            context.Books.Attach(item);
                            context.Books.Remove(item);
                            context.SaveChanges();
                            myDataGrid.ItemsSource = db.Books.Local.ToBindingList();
                        } else
                        {
                            MessageBox.Show("Книга на руках у читателя, не может быть удалена");
                        }
                    }
                }
            }/*
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
        public partial class AddDelBooks : Window
        {
            ARMebContext db;
            private readonly IRepositoryManager repository;

            public AddDelBooks()
            {
                InitializeComponent();
                db = new ARMebContext();
                List<Re> data = new List<Re>();

                var books = repository.Books.GetAllBooks(trackChanges: true).ToList();
                myDataGrid.ItemsSource = books;
            }

            private void insertBtn_Click(object sender, RoutedEventArgs e)
            {
                InsertPageBook page = new InsertPageBook();
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
    }
}
