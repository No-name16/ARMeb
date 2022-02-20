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
            /*using(var context = new UserContext()) //добавление в бд пользователя
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
            foreach (tblUser user in db.Users){
                if (user.Username == txtUsername.Text && user.Password == txtPassword.Text)
                {
                    Console.WriteLine(user.Username, " ", user.Password);
                    MainMenu window = new MainMenu();
                    window.Show();
                    this.Close();
                }
                
            }
            MessageBox.Show("Username or password are incorrect!");
            
        }
    }
}
