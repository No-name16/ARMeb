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
