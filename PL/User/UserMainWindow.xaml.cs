using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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

namespace PL.User;

/// <summary>
/// Interaction logic for UserMainWindow.xaml
/// </summary>
public partial class UserMainWindow : Window
{
    
    private BO.Cart cart = new() { Items = new() };
    public UserMainWindow()
    {
        InitializeComponent();
    }

    private void Products_Catalog_Click(object sender, RoutedEventArgs e)
    {
        new ProductItemListWindow(cart).Show();
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    private void Back_Btn_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Order_Tracking_Click(object sender, RoutedEventArgs e)
    {
        new OrderTracking().Show();
        Close();
    }
}
