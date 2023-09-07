using BlApi;
using PL.Utils;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PL.Admin.Order;


/// <summary>
/// Interaction logic for OrderWindow.xaml
/// </summary>
public partial class OrderWindow : Window, INotifyPropertyChanged
{
    private readonly IBl bl = Factory.Get() ?? throw new NullReferenceException("BlApi.Factory.Get() returned null");

    private static int id = 0;

    private static bool isAs = true;

    private static bool isUpdate = true;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged()
    {

        if (PropertyChanged != null)
        {
            var e = new PropertyChangedEventArgs("");
            PropertyChanged(this, e);
        }
    }

    private BO.Order? order;
    public BO.Order? Order 
    { 
        get {
            return order;
        }
        set {
            order = value;
            OnPropertyChanged();
        }
    }

    private BO.OrderForList? orderForList;

    public BO.OrderForList? OrderForList
    {
        get
        {
            return orderForList;
        }
        set
        {
            orderForList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<BO.OrderItem?>? items;
    public ObservableCollection<BO.OrderItem?>? Items
    {
        get
        {
            return items;
        }
        set
        {
            items = value;
            OnPropertyChanged();
        }
    }

    private void LoadOrder(int id)
    {
        OrderWindow.id = id;
        Order = bl.Order.GetOrder(OrderWindow.id) ?? new();
        Items = new(Order?.Items ?? new());
        OrderForList = BlUtils.Convertor.ToBoOrderForList(Order) ?? new();

    }

    public OrderWindow()
    {
        InitializeComponent();
    }

    public OrderWindow(int id, OrderWindowMode mode = OrderWindowMode.Update)
    {
        LoadOrder(id);
        InitializeComponent();
        Shiped_Btn.Visibility = Visibility.Hidden;
        Delivred_Btn.Visibility = Visibility.Hidden;
        if(mode == OrderWindowMode.View)
        {
            Update_Btn.Visibility = Visibility.Hidden;
        }
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    private void Back_Btn_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void TotalPrice_GVCH_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(OrderItem_LV.ItemsSource);
        view.SortDescriptions.Clear();
        if (isAs)
        {
            view.SortDescriptions.Add(new SortDescription("ProductId", ListSortDirection.Descending));
            isAs = false;
        }
        else
        {
            view.SortDescriptions.Add(new SortDescription("ProductId", ListSortDirection.Ascending));
            isAs = true;
        }
    }

    private void Amount_GVCH_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(OrderItem_LV.ItemsSource);
        view.SortDescriptions.Clear();
        if (isAs)
        {
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
            isAs = false;
        }
        else
        {
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            isAs = true;
        }
    }

    private void Status_GVCH_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(OrderItem_LV.ItemsSource);
        view.SortDescriptions.Clear();
        if (isAs)
        {
            view.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Descending));
            isAs = false;
        }
        else
        {
            view.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));
            isAs = true;
        }

    }

    private void Name_GVCH_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(OrderItem_LV.ItemsSource);
        view.SortDescriptions.Clear();
        if (isAs)
        {
            view.SortDescriptions.Add(new SortDescription("Amount", ListSortDirection.Descending));
            isAs = false;
        }
        else
        {
            view.SortDescriptions.Add(new SortDescription("Amount", ListSortDirection.Ascending));
            isAs = true;
        }

    }

    private void ID_GVCH_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(OrderItem_LV.ItemsSource);
        view.SortDescriptions.Clear();
        if (isAs)
        {
            view.SortDescriptions.Add(new SortDescription("TotalPrice", ListSortDirection.Descending));
            isAs = false;
        }
        else
        {
            view.SortDescriptions.Add(new SortDescription("TotalPrice", ListSortDirection.Ascending));
            isAs = true;
        }
    }

    private void Update_Btn_Click(object sender, RoutedEventArgs e)
    {
        if(isUpdate)
        {
            if (Order?.Shipped == null)
            {
                Shiped_Btn.Visibility = Visibility.Visible;
            }
            if (Order?.Delivered == null)
            {
                Delivred_Btn.Visibility = Visibility.Visible;
            }
            Update_Btn.Content = "Update Order";
            isUpdate = false;
        } else
        {
            Shiped_Btn.Visibility = Visibility.Hidden;
            Delivred_Btn.Visibility = Visibility.Hidden;
            Update_Btn.Content = "Done";
            isUpdate = true;
        }
    }

    private void Shiped_Btn_Click(object sender, RoutedEventArgs e)
    {
        //MessageBox.Show(Order.ToString());

        bl.Order.SetSendOrderForAdmin(id);
        Order = bl.Order.GetOrder(id);
        //LoadOrder(id);
        isUpdate = true;
    }

    private void Delivred_Btn_Click(object sender, RoutedEventArgs e)
    {
        if(Order?.Shipped == null)
        {
            bl.Order.SetSendOrderForAdmin(id);
        }
        bl.Order.SetSendOrderDeliveredForAdmin(id);
        Order = bl.Order.GetOrder(id);

        //LoadOrder(id);
        isUpdate = true;
    }

    private void OrderItem_LV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var item = OrderItem_LV.SelectedItem;
        if (item != null)
        {
            new ProductWindow(((BO.OrderItem)item).ProductId, ProductWindowMode.View).Show();
        }
    }
}
