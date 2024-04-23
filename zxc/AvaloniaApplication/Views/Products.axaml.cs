using Avalonia.Controls;

namespace AvaloniaApplication.Views
{
    public partial class Products : UserControl
    {
        public Products()
        {
            InitializeComponent();
            GeneredItems();
        }

        public void GeneredItems()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Product product = new Product();
                    product.plus.IsVisible = false;
                    product.minus.IsVisible = false;
                    product.buttonCart.IsVisible = false;
                    product.buttonOrders.IsVisible = false;
                    product.menuOrders.IsVisible = false;
                    Grid.SetRow(product, i);
                    Grid.SetColumn(product, j);
                    GridForProducts.Children.Add(product);
                }
            }
        }
    }
}
