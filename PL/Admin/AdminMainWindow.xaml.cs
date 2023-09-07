using PL.Admin.Order;
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

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
        }

        private void Product_Btn_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().Show();
        }

        private void Order_Btn_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow().Show();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Back_Btn_Click(object sender, RoutedEventArgs e)
        {            
            new MainWindow().Show();
            Close();
        }
        
    }
}
