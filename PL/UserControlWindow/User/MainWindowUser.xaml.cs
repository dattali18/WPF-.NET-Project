using System.ComponentModel;
using System.Windows;

namespace PL.UserControlWindow.User;

/// <summary>
/// Interaction logic for MainWindowUser.xaml
/// </summary>
public partial class MainWindowUser : Window, INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged()
    {

        if (PropertyChanged != null)
        {
            var e = new PropertyChangedEventArgs("");
            PropertyChanged(this, e);
        }
    }

    private BO.Cart? cart = new();

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
    public MainWindowUser()
    {
        cart.Items = new();

        InitializeComponent();

        uc1.Content = new ProductCatalogeWindow(Cart);
        uc2.Content = new PL.UserControlWindow.User.CartWindow(Cart);
    }

    //private void ProductCatalogeTab_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    //{
    //    Cart = ((PL.UserControlWindow.User.CartWindow)uc2.Content).GetCart();
    //    uc1.Content = new ProductCatalogeWindow(Cart);
    //}

    //private void CartTab_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    //{
    //    Cart = ((ProductCatalogeWindow)uc1.Content).GetCart();
    //    uc2.Content = new PL.User.CartWindow(Cart);
    //}

    private void Window_Activated(object sender, System.EventArgs e)
    {
        uc1.Content = new ProductCatalogeWindow(Cart);
        uc2.Content = new PL.UserControlWindow.User.CartWindow(Cart);
    }

    private void ProductCataglogeSp_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Cart = ((PL.UserControlWindow.User.CartWindow)uc2.Content).GetCart();
        uc1.Content = new ProductCatalogeWindow(Cart);
    }

    private void CartSP_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Cart = ((ProductCatalogeWindow)uc1.Content).GetCart();
        uc2.Content = new PL.UserControlWindow.User.CartWindow(Cart);
    }
}
