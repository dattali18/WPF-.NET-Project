using DalApi;
using PL.User;
using PL.Utils;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace PL;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private readonly BlApi.IBl bl = BlApi.Factory.Get() ?? throw new NullReferenceException("BlApi.Factory.Get() returned null");
    
    private readonly ProductWindowMode Mode;

    private readonly int ID;

    public ProductWindow(int id = 0, ProductWindowMode mode = ProductWindowMode.Add)
    {
        InitializeComponent();
        Categories.ItemsSource = Enum.GetValues(typeof(BO.ProductCategories));
        Mode = mode;
        if(Mode ==  ProductWindowMode.Update || Mode == ProductWindowMode.View)
        {
            // putting the data of the Products in the form
            BO.Product? prod = Data.GetProduct(id) ?? throw new DalApi.ObjectNotFoundException("from ProductWindow(int)");
            ProductID_TxtBx.Text = prod.ID.ToString();
            ProductName_TxtBx.Text = prod.Name;
            ProductPrice_TxtBx.Text = prod.Price.ToString();
            Categories.Text = prod.Categorie.ToString();
            ProductAmount_TxtBx.Text = prod.InStock.ToString();
            AddProduct_Btn.Content = "Update Product";
            ID = id;
            ProductID_TxtBx.IsEnabled = false;
            if (Mode == ProductWindowMode.View)
            {
                ProductName_TxtBx.IsEnabled = false;
                ProductPrice_TxtBx.IsEnabled = false;
                Categories.IsEnabled = false;
                ProductAmount_TxtBx.IsEnabled = false;
                AddProduct_Btn.Visibility = Visibility.Hidden;
            }
        }
    }

    private void AddProduct_Btn_Click(object sender, RoutedEventArgs e)
    {
        // checking the validity of the id form
        bool flag = int.TryParse(ProductID_TxtBx.Text, out int Id);
        if (!flag)
        {
            MessageBox.Show("The amount needs to be a number", "Error Message");
            return;
        }
        else if (Id < 100000 || Id > 1000000)
        {
            MessageBox.Show("Format Error: the ID need to be in the format xxxxxxx", "Error Message");
            return;
        }
        else if (bl.Product.GetProductForListForAdmin().Any(p => p?.ID == Id) && Mode == ProductWindowMode.Add)
        {
            MessageBox.Show($"The ID {Id} allready exist in the store", "Error Message");
            return;
        }
        // checking the validity of the name form
        string name = ProductName_TxtBx.Text;
        if (name == string.Empty)
        {
            MessageBox.Show("The Name can't be empty", "Error Message");
            return;
        }
        if (Categories.Text == string.Empty)
        {
            MessageBox.Show("You Have to choose a category", "Error Message");
            return;
        }
        // checking the validity of the price form
        flag = double.TryParse(ProductPrice_TxtBx.Text, out double price);
        if (!flag)
        {
            MessageBox.Show("The price needs to be a number", "Error Message");
            return;
        }
        else if (price <= 0)
        {
            MessageBox.Show("The price needs to be a number greater than 0", "Error Message");
            return;
        }
        // checking the validity of the amount form
        flag = int.TryParse(ProductAmount_TxtBx.Text, out int amount);
        if (!flag)
        {
            MessageBox.Show("The amount needs to be a number", "Error Message");
            return;
        }
        else if (amount < 0)
        {
            MessageBox.Show("The amount needs to be a number greater than 0", "Error Message");
            return;
        }        

        BO.ProductCategories categories = CategoriesConvertor.FromTextCategories(Categories.Text);

        if (Mode == ProductWindowMode.Add)
        {
            try
            {
                bl.Product.SetAddProductForAdmin(new BO.Product() { ID = Id, Categorie = categories, InStock = amount, Name = name, Price = price });
            } catch (DuplicateIDException)
            {
                MessageBox.Show("The ID Is Already Existing", "Error Message");
                return;
            }
            
        }
        else if (Mode == ProductWindowMode.Update)
        {
            bl.Product.SetUpdateProductForAdmin(new BO.Product() { ID = ID, Categorie = categories, InStock = amount, Name = name, Price = price });
        }

        //new ProductListWindow().Show();
        Close(); 
    }

    // Bonus
    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void Back_Btn_Click(object sender, RoutedEventArgs e)
    {
        //if (Mode == ProductWindowMode.Add || Mode == ProductWindowMode.Update)
        //    new ProductListWindow().Show();
        Close();
    }

    private void ProductWindow_MouseDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    private void ProductName_Txbx_Change(object sender, RoutedEventArgs e)
    {
        if (ProductName_TxtBx.Text == string.Empty)
        {
            ProductName_TxtBx.BorderBrush = System.Windows.Media.Brushes.Red;
        }
        else
        {
            ProductName_TxtBx.BorderBrush = System.Windows.Media.Brushes.Green;
        }
    }

    private void ProductPrice_Txtbx_Change(object sender, RoutedEventArgs e)
    {
        if (ProductPrice_TxtBx.Text == string.Empty)
        {
            ProductPrice_TxtBx.BorderBrush = System.Windows.Media.Brushes.Red;
        }
        else if (int.Parse(ProductPrice_TxtBx.Text) <= 0)
        {
            ProductPrice_TxtBx.BorderBrush = System.Windows.Media.Brushes.Red;
        }
        else
        {
            ProductPrice_TxtBx.BorderBrush = System.Windows.Media.Brushes.Green;
        }
    }

    private void ProductAmount_Txtbx_Change(object sender, RoutedEventArgs e)
    {
        if (ProductAmount_TxtBx.Text == string.Empty)
        {
            ProductAmount_TxtBx.BorderBrush = System.Windows.Media.Brushes.Red;
        }
        else if (int.Parse(ProductAmount_TxtBx.Text) < 0)
        {
            ProductAmount_TxtBx.BorderBrush = System.Windows.Media.Brushes.Red;
        }
        else
        {
            ProductAmount_TxtBx.BorderBrush = System.Windows.Media.Brushes.Green;
        }
    }
}
