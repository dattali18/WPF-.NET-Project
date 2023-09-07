using BlApi;
using PL.Utils;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Linq;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace PL.Admin.Order;

/// <summary>
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class OrderListWindow : Window, INotifyPropertyChanged
{
    private readonly IBl bl = Factory.Get() ?? throw new NullReferenceException("BlApi.Factory.Get() returned null");

    private readonly static PropertyGroupDescription nameGroupDescription = new("Name", new NameConvertor());
    private readonly static PropertyGroupDescription statusGroupDescription = new("Status");
    private readonly static PropertyGroupDescription priceGroupDescription = new("TotalPrice", new PriceConvertor());

    private bool isAs = true;

    private ObservableCollection<BO.OrderForList?>? ordList;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged()
    {

        if (PropertyChanged != null)
        {
            var e = new PropertyChangedEventArgs("");
            PropertyChanged(this, e);
        }
    }

    public ObservableCollection<BO.OrderForList?>? OrdList
    {
        get
        {
            return ordList;
        }
        set
        {
            ordList = value;
            OnPropertyChanged();
        }
    }

    public OrderListWindow()
    {
        OrdList = new(
                from order in bl.Order.GetOrdersListForAdmin()
                where order != null
                orderby order?.ID 
                select order
            );

        InitializeComponent();

        //Orders_LV.ItemsSource = Data.GetOrders();

        OrderCategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));

        OrderCategoriesSelector.SelectedIndex = 4;

        //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Orders_LV.ItemsSource);
        //view.SortDescriptions.Clear();
        //view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
    }

    private void OrderCategoriesSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        var p = (BO.OrderStatus)OrderCategoriesSelector.SelectedItem;

        if (p == BO.OrderStatus.All)
        {
            OrdList = new(
                            from order in bl.Order.GetOrdersListForAdmin()
                            where order != null
                            orderby order?.ID
                            select order
                        );
            //Orders_LV.ItemsSource = OrdList;
        }
        else
        {
            OrdList = new(
                        from order in bl.Order.GetOrdersListForAdmin()
                        where order != null && order.Status == p
                        orderby order?.ID
                        select order
                    );
            //List<BO.OrderForList?> list = new(Data.GetOrders());
            //Orders_LV.ItemsSource = OrdList;
        }
    }

    private void ID_GVCH_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Orders_LV.ItemsSource);
        view.SortDescriptions.Clear();
        if (isAs)
        {
            view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Descending));
            isAs = false;
        }
        else
        {
            view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
            isAs = true;
        }
    }

    private void Name_GVCH_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Orders_LV.ItemsSource);
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
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Orders_LV.ItemsSource);
        view.SortDescriptions.Clear();
        if (isAs)
        {
            view.SortDescriptions.Add(new SortDescription("Status", ListSortDirection.Descending));
            isAs = false;
        }
        else
        {
            view.SortDescriptions.Add(new SortDescription("Status", ListSortDirection.Ascending));
            isAs = true;
        }
    }

    private void Amount_GVCH_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Orders_LV.ItemsSource);
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

    private void TotalPrice_GVCH_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Orders_LV.ItemsSource);
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

    private void Orders_LV_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var item = Orders_LV.SelectedItem;
        if (item != null)
        {
            new OrderWindow(((BO.OrderForList)item).ID).Show();
            Close();
        }
    }

    private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    private void Name_ChBx_Checked(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Orders_LV.ItemsSource);
        view.GroupDescriptions.Add(nameGroupDescription);
    }

    private void Name_ChBx_Unchecked(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Orders_LV.ItemsSource);
        view.GroupDescriptions.Remove(nameGroupDescription);
    }

    private void Status_ChBx_Checked(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Orders_LV.ItemsSource);
        view.GroupDescriptions.Add(statusGroupDescription);
    }

    private void Status_ChBx_Unchecked(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Orders_LV.ItemsSource);
        view.GroupDescriptions.Remove(statusGroupDescription);
    }

    private void Price_ChBx_Checked(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Orders_LV.ItemsSource);
        view.GroupDescriptions.Add(priceGroupDescription);
    }

    private void Price_ChBx_Unchecked(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Orders_LV.ItemsSource);
        view.GroupDescriptions.Remove(priceGroupDescription);
    }
}
