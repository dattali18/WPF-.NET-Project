using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.UserControlWindow.User;

/// <summary>
/// Interaction logic for ProductCatalogeWindow.xaml
/// </summary>
public partial class ProductCatalogeWindow : UserControl, INotifyPropertyChanged
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

    private ObservableCollection<BO.ProductItem?>? cataloge;

    public ObservableCollection<BO.ProductItem?>? Cataloge
    {
        get
        {
            return cataloge;
        }
        set
        {
            cataloge = value;
            OnPropertyChanged();
        }
    }

    public BO.Cart? GetCart()
    {
        return Cart;
    }

    public ProductCatalogeWindow(BO.Cart? cart)
    {
        Cataloge = new(
                   from prod in bl.Product.GetProductListForAdmin()
                   where prod != null
                   orderby prod.ID
                   select BlUtils.Convertor.ToBoProductItem(prod, Cart)
                   );
        Cart = cart;

        InitializeComponent();

        //Products_LV.ItemsSource = from prod in bl.Product.GetProductListForAdmin()
        //                          where prod != null
        //                          orderby prod.ID
        //                          select BlUtils.Convertor.ToBoProductItem(prod);

        ProductsCategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.ProductCategories));

        ProductsCategoriesSelector.SelectedIndex = 5;

    }

    private void ProductsCategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        var p = (BO.ProductCategories)ProductsCategoriesSelector.SelectedItem;

        if (p == BO.ProductCategories.All)
        {
            Cataloge = new(
                            from prod in bl.Product.GetProductListForAdmin()
                            where prod != null
                            orderby prod.ID
                            select BlUtils.Convertor.ToBoProductItem(prod, Cart)
                        );
        }
        else
        {
            Cataloge = new(
                            from prod in bl.Product.GetProductListForAdmin()
                            where prod != null && prod?.Categorie == p
                            orderby prod.ID
                            select BlUtils.Convertor.ToBoProductItem(prod, Cart)
                        );
        }
        //UpdateAmountInProductItem();
    }

    private void Products_LV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var item = Products_LV.SelectedItem;
        if (item != null)
        {
            new ProductWindow(((BO.ProductItem)item).ID, Utils.ProductWindowMode.View).Show();
        }
    }

    private void ID_GVCH_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Products_LV.ItemsSource);
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
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Products_LV.ItemsSource);
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

    private void Categorie_GVCH_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Products_LV.ItemsSource);
        view.SortDescriptions.Clear();
        if (isAs)
        {
            view.SortDescriptions.Add(new SortDescription("Categories", ListSortDirection.Descending));
            isAs = false;
        }
        else
        {
            view.SortDescriptions.Add(new SortDescription("Categories", ListSortDirection.Ascending));
            isAs = true;
        }
    }

    private void Price_GVCH_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Products_LV.ItemsSource);
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

    private void InStock_GVCH_Click(object sender, RoutedEventArgs e)
    {
        //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Products_LV.ItemsSource);
        //view.SortDescriptions.Clear();
        //if (isAs)
        //{
        //    view.SortDescriptions.Add(new SortDescription("InStock", ListSortDirection.Descending));
        //    isAs = false;
        //}
        //else
        //{
        //    view.SortDescriptions.Add(new SortDescription("InStock", ListSortDirection.Ascending));
        //    isAs = true;
        //}
        if (isAs)
        {
            Cataloge = new(
                from prod in bl.Product.GetProductListForAdmin()
                where prod != null && prod.InStock > 0
                orderby prod.ID
                select BlUtils.Convertor.ToBoProductItem(prod, Cart)
                );
            isAs = false;
        }
        else
        {
            Cataloge = new(
                from prod in bl.Product.GetProductListForAdmin()
                where prod != null
                orderby prod.ID
                select BlUtils.Convertor.ToBoProductItem(prod, Cart)
                );
            isAs = true;
        }
        // UpdateAmountInProductItem();
    }


    private void AddProduct_Click(object sender, RoutedEventArgs e)
    {
        bool _ = int.TryParse(((Button)sender).Tag.ToString(), out int id);
        var item = bl.Product.GetProduct(id);
        if (item == null || item?.InStock <= 0)
        {
            MessageBox.Show($"We are sorry the product you are trying to add to your cart is not available");
            //Cataloge = new(
            //            from prod in bl.Product.GetProductListForAdmin()
            //            where prod != null
            //            orderby prod.ID
            //            select BlUtils.Convertor.ToBoProductItem(prod)
            //            );
            return;
        }
        if (MessageBox.Show($"Are you sure you want to add '{item?.Name}' to the cart?", "Success", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            try
            {
                Cart = bl.Cart.SetAddProductForUsers(Cart, id) ?? new();

                //var prod = Cataloge?.FirstOrDefault(item => item?.ID == id);
                var cat = Cataloge?.ToList() ?? new();
                cat.Find(item => item?.ID == id).Amount += 1;
                Cataloge = new(cat);

                //Cataloge = new(
                //            from prod in bl.Product.GetProductListForAdmin()
                //            where prod != null
                //            orderby prod.ID
                //            select BlUtils.Convertor.ToBoProductItem(prod, Cart)
                //            );
                //UpdateAmountInProductItem();
            }
            catch (OutOfStockException)
            {
                MessageBox.Show($"We are sorry the product you are trying to add to your cart is not available");
            }

        }

    }

    private void Amount_GVCH_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Products_LV.ItemsSource);
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

    private void UpdateAmountInProductItem()
    {
        if (Cart?.Items == null)
            return;
        foreach (var item in Cart.Items)
        {
            var prod = Cataloge?.FirstOrDefault(it => it?.ID == item?.ProductID);
            if (prod == null)
                return;
            Cataloge?.Remove(prod);
            prod.Amount = item?.Amount ?? 0;
            Cataloge?.Add(prod);
            var cat = Cataloge;
            Cataloge = new(
                        from i in cat
                        where i != null
                        orderby i?.ID
                        select i
                       );
        }
    }

    private void Grouping_CheckBx_Unchecked(object sender, RoutedEventArgs e)
    {
        var cat = Cataloge;
        Cataloge = new(
                       from i in cat
                       where i != null
                       orderby i?.ID
                       select i
                      );
    }

    private void Grouping_CheckBx_Checked(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Products_LV.ItemsSource);
        view.GroupDescriptions.Add(new PropertyGroupDescription("Categories"));
    }
}

