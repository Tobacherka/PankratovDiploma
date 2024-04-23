using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaApplication.Views
{
    public partial class SettingsProduct : UserControl
    {
        public SettingsProduct()
        {
            InitializeComponent();
            GeneredItem();
        }

        public void GeneredItem()
        {
            Product product = new Product();
            product.plus.IsVisible = false;
            product.minus.IsVisible = false;
            product.buttonCart.IsVisible = false;
            product.buttonOrders.IsVisible = false;
            product.menuOrders.IsVisible = false;
            product.category.IsEnabled = true;
            product.name.IsEnabled = true;
            product.price.IsEnabled = true;
            product.border.CornerRadius = new Avalonia.CornerRadius(30);
            product.border.BoxShadow = new BoxShadows(
                new BoxShadow
                {
                    OffsetX = 0,
                    OffsetY = 0,
                    Blur = 25,
                    Spread = 0,
                    Color = Color.Parse("#7f000000"),

                }
            );
            product.grid.Margin = new Thickness(25);
            product.category.FontSize = 11;
            product.name.FontSize = 13;
            product.price.FontSize = 12;
            product.button.CornerRadius = new Avalonia.CornerRadius(7);
            product.button.Padding = new Thickness(10, 5);
            product.button.FontSize = 13;
            product.textblock.Text = "Добавить в корзину";
            Grid.SetColumn(product, 0);
            Grid.SetRow(product, 0);
            Grid.SetRowSpan(product, 2);
            GridForSettingProduct.Children.Add(product);
        }
    }
}
