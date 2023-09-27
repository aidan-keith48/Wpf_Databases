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
using Wpf_Databases.Classes;
using Wpf_Databases.DataController;

namespace Wpf_Databases
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        dbController db = new dbController();
        public MainWindow()
        {
            InitializeComponent();
            //dataGrid.ItemsSource = Reviews;
        }

        private void viewDataBtn_Click(object sender, RoutedEventArgs e)
        {
            Reviews review = new Reviews();

            displayTxt.Text = db.displayData();
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

            db.addReview(review);
            MessageBox.Show("Values Added");

        }
    }
}
