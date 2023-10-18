using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using Wpf_Databases.Classes;
using Wpf_Databases.DataController;

namespace Wpf_Databases
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<User> userList = new List<User>();

        dbController db = new dbController();
        public MainWindow()
        {
            InitializeComponent();
            //List<int> userIds = db.GetUser();
            ObservableCollection<string> userIds = db.GetUser();
            //var user = db.getUser();
            selectUserCombo.ItemsSource = userIds;
            //dataGrid.ItemsSource = Reviews;
        }

        public MainWindow(List<User> userList)
        {
            InitializeComponent();
            this.userList = userList;
            ObservableCollection<string> userIds = db.GetUser();
            //var user = db.getUser();
            selectUserCombo.ItemsSource = userIds;
        }

        private void viewDataBtn_Click(object sender, RoutedEventArgs e)
        {
            Reviews review = new Reviews();
            //string user = (string)selectUserCombo.SelectedValue;
            dbController db = new dbController();

            DataTable dt = db.displayData(userList);
            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            Reviews review = new Reviews();
            //Create local vars to store value from textboxes

            string name = nameTxt.Text;
            string email = emailTxt.Text;
            string product = productTxt.Text;
            string comment = reviewTxt.Text;

            review = new Reviews(name, email, product, comment);

            db.addReview(review, userList);
            MessageBox.Show("Values Added");

        }

        private void selectAll_Click(object sender, RoutedEventArgs e)
        {
            Reviews review = new Reviews();
           // int id = Convert.ToInt32(selectUserCombo.SelectedValue);
            dbController db = new dbController();

            DataTable dt = db.displayAll();
            dataGrid.ItemsSource = dt.DefaultView;
        }
    }
}
