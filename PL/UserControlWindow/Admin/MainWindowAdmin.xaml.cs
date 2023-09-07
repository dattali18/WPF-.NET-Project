using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace PL.UserControlWindow.Admin;

/// <summary>
/// Interaction logic for MainWindowAdmin.xaml
/// </summary>
public partial class MainWindowAdmin : Window
{
    public MainWindowAdmin()
    {
        InitializeComponent();

        uc1.Content = new PL.UserControlWindow.Admin.Products.ProductListWindow();
        uc2.Content = new PL.UserControlWindow.Admin.Orders.OrderListWindow();
    }



    private void ProductListSP_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        uc2.Content = new PL.UserControlWindow.Admin.Orders.OrderListWindow();
    }

    private void OrderListSp_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        uc1.Content = new PL.UserControlWindow.Admin.Products.ProductListWindow();
    }

    private void Window_Activated(object sender, EventArgs e)
    {
        uc1.Content = new PL.UserControlWindow.Admin.Products.ProductListWindow();
        uc2.Content = new PL.UserControlWindow.Admin.Orders.OrderListWindow();
    }
}
