using BlApi;
using PL.Admin;
using PL.Admin.Order;
using PL.Simulator;
using PL.User;
using PL.UserControlWindow.Admin;
using System;
using System.Windows;


namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IBl bl = BlApi.Factory.Get() ?? throw new NullReferenceException("MainWindow");
    public MainWindow()
    {
        InitializeComponent();
    }
    private void MainWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    private void Admin_Click(object sender, RoutedEventArgs e)
    {
        //new AdminMainWindow().Show();
        //Close();
        new MainWindowAdmin().Show();
    }

    private void User_Click(object sender, RoutedEventArgs e)
    {
        new UserMainWindow().Show();
        //Close();
    }

    private void Simulator_Click(object sender, RoutedEventArgs e)
    {
        new simulatorWindow().Show();
    }
}
