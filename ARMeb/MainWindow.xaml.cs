using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using ARMeb.Models;
using System.Data.Entity;


namespace ARMeb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ARMebContext db;
        public MainWindow()
        {
            InitializeComponent();
            db = new ARMebContext();
            db.Users.Load(); // загружаем данны
        }

        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            /*using(var context = new ARMebContext()) //добавление в бд пользователя
             {
                 var group = new tblUser()
                 {
                     Id = 2,
                     Username = "Vika",
                     Password = "1234",
                 };
                 context.Users.Add(group);

                 context.SaveChanges();
             }*/
            /*using (var context = new ARMebContext()) //добавление в бд пользователя
            {
                var group = new Readers()
                {
                    Id = 5,
                    Name = "Vika",
                    Age = 16,
                    HaveBooks = false,
                };
                context.Readers.Add(group);

                context.SaveChanges();
            }*/

            using (var context = new ARMebContext()) //добавление в бд пользователя
            {
                var group = new Operations()
                {
                    Time = DateTime.Now,
                    Title = "Вход выполнен"
                };
                context.Listoperations.Add(group);

                context.SaveChanges();
            }
            bool incorrect = true;
            foreach (tblUser user in db.Users){
                if (user.Username == txtUsername.Text && user.Password == txtPassword.Text)
                {
                    MainMenu window = new MainMenu();
                    window.Show();
                    this.Close();
                    incorrect = false;
                    break;
                }  
            }
            if (incorrect)
            {
                MessageBox.Show("Username or password are incorrect!");
            }
        }
    }
}
