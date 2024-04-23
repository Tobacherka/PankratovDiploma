using Avalonia.Controls;
using AvaloniaApplication.Classes;

namespace AvaloniaApplication.Views
{
    public partial class Cart : UserControl
    {
        public Cart()
        {
            InitializeComponent();
            GeneredItems();
        }

        public void GeneredItems()
        {
            for (int i = 0; i < 6; i++)
            {
                Product product = new Product();
                product.button.IsVisible = false;
                product.menuOrders.IsVisible = false;
                product.buttonOrders.IsVisible = false;
                Grid.SetRow(product, 0);
                Grid.SetColumn(product, i);
                GridForCart.Children.Add(product);
            }
        }
    }
}
