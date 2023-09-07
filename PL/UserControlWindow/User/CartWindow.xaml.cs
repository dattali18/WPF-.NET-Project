using BlApi;
using PL.User;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PL.UserControlWindow.User
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : UserControl, INotifyPropertyChanged
    {
        private readonly IBl bl = Factory.Get() ?? throw new NullReferenceException("BlApi.Factory.Get() returned null");

        private bool isAs = true;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged()
        {

            if (PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs("");
                PropertyChanged(this, e);
            }
        }

        private BO.Cart? cart;

        public BO.Cart? Cart
        {
            get
            {
                return cart;
            }
            set
            {
                cart = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DO.OrderItem?>? orderItems;

        public ObservableCollection<DO.OrderItem?>? OrderItems
        {
            get
            {
                return orderItems;
            }
            set
            {
                orderItems = value;
                OnPropertyChanged();
            }
        }

        public BO.Cart? GetCart()
        {
            return Cart;
        }


        public CartWindow() => InitializeComponent();

        public CartWindow(BO.Cart? crt)
        {

            Cart = crt;
            OrderItems = new(
                from ordItem in Cart?.Items
                where ordItem != null
                orderby ordItem?.ProductID
                select ordItem
            );

            InitializeComponent();


            //UpGrid.DataContext = Cart;

            //OrderItem_LV.ItemsSource = orderItems;
        }

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            bool flag = int.TryParse(((Button)sender).Tag.ToString(), out int id);
            if (flag)
            {
                var prod = bl.Product.GetProduct(id);
                var amount = Cart?.Items?.FirstOrDefault(item => item?.ProductID == id)?.Amount ?? 0;
                if (MessageBox.Show($"Are you sure you want to remove '{prod?.Name}' from the cart?", "Success", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Cart = bl.Cart.SetAddAmountOfProductForUsers(Cart, id, amount - 1) ?? new();
                        OrderItems = new(
                                from ordItem in Cart?.Items
                                where ordItem != null
                                orderby ordItem?.ProductID
                                select ordItem
                        );
                    } catch(OutOfStockException)
                    {
                        MessageBox.Show("We are sorry we can't add another of this product in your cart because we are out of stock");
                    }
                    
                    //OrderItem_LV.Items.Refresh();
                }

            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            bool flag = int.TryParse(((Button)sender).Tag.ToString(), out int id);
            if (flag)
            {
                var prod = bl.Product.GetProduct(id);
                var amount = Cart?.Items?.FirstOrDefault(item => item?.ProductID == id)?.Amount ?? 0;
                if (MessageBox.Show($"Are you sure you want to add another '{prod?.Name}' to the cart?", "Success", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Cart = bl.Cart.SetAddAmountOfProductForUsers(Cart, id, amount + 1) ?? new();
                        OrderItems = new(
                                from ordItem in Cart?.Items
                                where ordItem != null
                                orderby ordItem?.ProductID
                                select ordItem
                        );
                    }
                    catch (OutOfStockException)
                    {
                        MessageBox.Show("We are sorry we can't add another of this product in your cart because we are out of stock");
                    }


                    //OrderItem_LV.Items.Refresh();
                }

            }
        }

        private void Buy_btn_Click(object sender, RoutedEventArgs e)
        {
            new BuyWindow(Cart).Show();
            // close the window
        }

        private void TotalPrice_GVCH_Click(object sender, RoutedEventArgs e)
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

        private void Amount_GVCH_Click(object sender, RoutedEventArgs e)
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

        private void Status_GVCH_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(OrderItem_LV.ItemsSource);
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

        private void Name_GVCH_Click(object sender, RoutedEventArgs e)
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

        private void ID_GVCH_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(OrderItem_LV.ItemsSource);
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

        private void OrderItem_LV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
