using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


using System.ComponentModel;

using BlImplementation;
using BlApi;
using PL.Utils;
using System.Linq;
using System.Collections.ObjectModel;

namespace PL;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window, INotifyPropertyChanged
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

    private ObservableCollection<BO.ProductForList?>? prodList;
    public ObservableCollection<BO.ProductForList?>? ProdList
    {
        get
        {
            return prodList;
        }
        set
        {
            prodList = value;
            OnPropertyChanged();
        }
    }

    public ProductListWindow()
    {
        ProdList = new(
                   from prod in bl.Product.GetProductForListForAdmin()
                   where prod != null
                   orderby prod?.ID
                   select prod
                   );

        InitializeComponent();

        //Products_LV.ItemsSource = from prod in bl.Product.GetProductForListForAdmin()
        //                          where prod != null
        //                          orderby prod?.ID
        //                          select prod; 

        ProductsCategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.ProductCategories));

        ProductsCategoriesSelector.SelectedIndex = 5;
        }

    private void ProductsCategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var p = (BO.ProductCategories)ProductsCategoriesSelector.SelectedItem;

        if (p == BO.ProductCategories.All)
        {
            ProdList = new(
                            from prod in bl.Product.GetProductForListForAdmin()
                            where prod != null
                            orderby prod?.ID
                            select prod
                           );
            
        } else
        {
            ProdList = new(
                        from prod in bl.Product.GetProductForListForAdmin()
                        where prod != null && prod?.Categories == p
                        orderby prod?.ID
                        select prod
                        ); 
        }
        //Products_LV.ItemsSource = ProdList;
    }

    private void AddProduct_Btn_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow().Show();
        Close();
    }

    private void UpdateProduct_Double_click(object sender, MouseButtonEventArgs e)
    {
        var item = Products_LV.SelectedItem;
        if (item != null)
        {
            new ProductWindow(((BO.ProductForList)item).ID, ProductWindowMode.Update).Show();
            Close();
        }

    }

    private void Name_GV_Btn_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Products_LV.ItemsSource);
        view.SortDescriptions.Clear();
        if(isAs)
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

    private void ID_GV_Btn_Click(object sender, RoutedEventArgs e)
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

    private void Price_GV_Btn_Click(object sender, RoutedEventArgs e)
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


    private void Products_MouseDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }

}
