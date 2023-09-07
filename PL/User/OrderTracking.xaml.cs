using BlApi;
using PL.Admin.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PL.User;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class OrderTracking : Window, INotifyPropertyChanged
{
    private readonly BlApi.IBl bl = BlApi.Factory.Get() ?? throw new NullReferenceException("BlApi.Factory.Get() returned null");

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged()
    {

        if (PropertyChanged != null)
        {
            var e = new PropertyChangedEventArgs("");
            PropertyChanged(this, e);
        }
    }

    public OrderTracking()
    {
        InitializeComponent();
    }

    private void Back_Btn_Click(object sender, RoutedEventArgs e)
    {
        new UserMainWindow().Show();
        Close();
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    private void Search_Btn_Click(object sender, RoutedEventArgs e)
    {
        bool _ = int.TryParse(OrderId_TBx.Text, out int Id);
        try
        {
            BO.OrderTracking order = bl.Order.GetOrderTrackingForAdmin(Id) ?? throw new NullReferenceException("couldn't find the order");
            BO.Order orderForCheck = bl.Order.GetOrder(Id) ?? throw new NullReferenceException("couldn't find the order");

            if (OrderName_TBx.Text != orderForCheck.CostumerName || OrderEmail_TBx.Text != orderForCheck.CostumerEmail)
            {
                throw new AuthenticationFailException("Name or Mail is wrong");
            }
            orderBtn.Visibility = Visibility.Visible;
            MainStack.DataContext = order;

            var date1 = (order?.Values?[0])?.Item1;
            OrderPlaced_cd.SelectedDate = date1;
            OrderPlaced_cd.DisplayDate = date1 ?? DateTime.Now;

            var date2 = (order?.Values?[1])?.Item1;
            if(date2 == null) {
                OrderSent_cd.IsEnabled = false;
            } else
            {
                OrderSent_cd.IsEnabled = true;
            }
            OrderSent_cd.SelectedDate = date2;
            OrderSent_cd.DisplayDate = date2 ?? DateTime.Now;

            var date3 = (order?.Values?[2])?.Item1;
            if (date3 == null)
            {
                OrderArrived_cd.IsEnabled = false;
            } else
            {
                OrderArrived_cd.IsEnabled = true;
            }

            OrderArrived_cd.SelectedDate = date3;
            OrderArrived_cd.DisplayDate = date3 ?? DateTime.Now;

        } 
        catch(NullReferenceException)
        {
            MessageBox.Show($"We couldn't find the order {Id}");
        }
        catch(AuthenticationFailException)
        {
            MessageBox.Show($"Name or Email isn't match to the order {Id}");
        }
        
    }

    private void orderBtn_Click(object sender, RoutedEventArgs e)
    {
        bool _ = int.TryParse(OrderId_TBx.Text, out int Id);
        new OrderWindow(Id, Utils.OrderWindowMode.View).Show();
    }
}
