using BlApi;
using BO;
using DO;
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
using System.Windows.Shapes;

namespace PL.User
{
    /// <summary>
    /// Interaction logic for BuyWindow.xaml
    /// </summary>
    public partial class BuyWindow : Window
    {
        private readonly IBl bl = Factory.Get() ?? throw new NullReferenceException("BlApi.Factory.Get() returned null");

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

        public BuyWindow(BO.Cart? cart)
        {
            Cart = cart;
            InitializeComponent();
        }

        private void Back_Btn_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(Cart).Show();
            Close();
        }

        private void CreateOrder_Btn_Click(Object sender, RoutedEventArgs e)
        {
            // checking the validity of the name form
            string name = CostumerName_TxtBx.Text;
            if (name == string.Empty)
            {
                MessageBox.Show("The Name can't be empty", "Error Message");
                return;
            }
            if (name == "Name")
            {
                MessageBox.Show("Can't use default name", "Error Message");
                return;
            }

            // checking the validity of the mail form
            string mail = CostumerMail_TxtBx.Text;
            if (mail == string.Empty) 
            {
                MessageBox.Show("The mail can't be empty", "Error Message");
                return;
            }
            if (mail == "example@mail.com")
            {
                MessageBox.Show("Can't use default mail", "Error Message");
                return;
            }

            // checking the validity of the address form
            string address = CostumerAddress_TxtBx.Text;
            if (address == string.Empty) 
            {
                MessageBox.Show("The address can't be empty", "Error Message");
                return;
            }
            if (address == "Address")
            {
                MessageBox.Show("Can't use default mail", "Error Message");
                return;
            }
            Cart.CostumerName = name;
            Cart.CostumerEmail = mail;
            Cart.CostumerAddress = address;

            try
            {
                int id = bl.Cart.SetFinalizeOrderForUsers(Cart);
                MessageBox.Show($"Success!\nYour order number is {id}", "OK");
                Close();
            }
            catch (EmailNotValidException)
            {
                MessageBox.Show("Mail isn't valid", "Error Message");
                return;
            }
            catch (OutOfStockException)
            {
                MessageBox.Show("We are out of stock", "Error Message");
                return;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
