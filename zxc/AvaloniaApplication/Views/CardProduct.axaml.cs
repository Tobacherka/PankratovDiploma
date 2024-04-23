using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using static System.Net.Mime.MediaTypeNames;

namespace AvaloniaApplication.Views
{
    public partial class CardProduct : UserControl
    {
        public CardProduct()
        {
            InitializeComponent();
            GeneredItem();
            ChangeBtn1.Click += ChangeBtn1_Click;
            ChangeBtn2.Click += ChangeBtn2_Click;
            ChangeBtn3.Click += ChangeBtn3_Click;
        }

        private void ChangeBtn1_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (textbox1.IsEnabled == false)
                textbox1.IsEnabled = true;
            else textbox1.IsEnabled = false;
        }
        private void ChangeBtn2_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (textbox2.IsEnabled == false)
                textbox2.IsEnabled = true;
            else textbox2.IsEnabled = false;
        }
        private void ChangeBtn3_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (textbox3.IsEnabled == false)
                textbox3.IsEnabled = true;
            else textbox3.IsEnabled = false;
        }

        public void GeneredItem()
        {  
            Product product = new Product();
            product.plus.IsVisible = false;
            product.minus.IsVisible = false;
            product.buttonCart.IsVisible = false;
            product.buttonOrders.IsVisible = false;
            product.menuOrders.IsVisible = false;
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
            GridForCardProduct.Children.Add(product);   
        }
    }
}
