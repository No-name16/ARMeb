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
using System.Collections.Generic;
using System.Linq;

namespace ARMeb
{

    /// <summary>
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        ARMebContext db;
        public Search()
        {
            InitializeComponent();
            db = new ARMebContext();
            db.Books.Load();
            using (var context = new ARMebContext()) //добавление в бд пользователя
            {
                var group = new tblBook()
                {

                    NumOfBooks = 3,
                    IsAny = true,
                    BookAuthor = "Васильев А.Р.",
                    Bookname = "Журавли",
                    
                    
                };
                context.Books.Add(group);

                context.SaveChanges();
            }
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

            // Отложенный запрос
            var listbooks = db.Books.ToList();
            // Извлечь всех заказчиков и отобразить их имена в консоли
            List<tblBook> result = new List<tblBook> { };
            if ("" != BookAuthor.Text || "" != BookName.Text)
            {
                foreach (tblBook book in listbooks)
                {
                    if ((book.BookAuthor == BookAuthor.Text && book.Bookname == BookName.Text) || ("" == BookAuthor.Text && book.Bookname == BookName.Text) || (book.BookAuthor == BookAuthor.Text && "" == BookName.Text))
                    {
                        result.Add(book);
                    }
            }

            }else
            {
                result = listbooks;
            }
            bookGrid.ItemsSource = result;
        }
       
    }
}
