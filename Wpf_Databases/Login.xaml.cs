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
using System.Windows.Shapes;
using Wpf_Databases.Classes;
using Wpf_Databases.DataController;

namespace Wpf_Databases
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        dbController db = new dbController();
        private List<User> userList = new List<User>();
        public Login()
        {
            InitializeComponent();
        }

        public Login(List<User> newUserList)
        {
            InitializeComponent();
            this.userList = newUserList;
        }

        public void AddUser()
        {
            string userName = username.Text;
            db.addUserInfo(userName);
            MessageBox.Show("User has been added to database");           

            userList.Add(new User(userName) {Username = userName});
        }

        public void AddUserLogin()
        {
            string userName = username.Text;    
            userList.Add(new User(userName) { Username = userName });
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddUser();
        }

        private void loginC(object sender, RoutedEventArgs e)
        {
            bool busy = true;
            AddUserLogin();
            db.doesUserExist(username.Text);

            MainWindow mw = new MainWindow(userList);
            mw.Show();
            this.Close();
        }
    }
}
