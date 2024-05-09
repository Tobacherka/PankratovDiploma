using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using AvaloniaApplication.Classes;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace AvaloniaApplication.Views
{
    public partial class CardProduct : UserControl
    {
        public Product ProductControl { get; set; }
        //public DbProduct? Product {  get; private set; }
        public CardProduct(Product product)
        {
            ProductControl = product;
            //Product = GlobalBuffer.Products?.FirstOrDefault(x => x.Id == product.Id);
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
            ProductControl.plus.IsVisible = false;
            ProductControl.minus.IsVisible = false;
            ProductControl.buttonCart.IsVisible = false;
            ProductControl.buttonOrders.IsVisible = false;
            ProductControl.menuOrders.IsVisible = false;
            ProductControl.border.CornerRadius = new Avalonia.CornerRadius(30);
            ProductControl.border.BoxShadow = new BoxShadows(
                new BoxShadow
                {
                    OffsetX = 0,
                    OffsetY = 0,
                    Blur = 25,
                    Spread = 0,
                    Color = Color.Parse("#7f000000"),

                }
            );
            ProductControl.grid.Margin = new Thickness(25);
            ProductControl.category.FontSize = 11;
            ProductControl.name.FontSize = 13;
            ProductControl.price.FontSize = 12;
            ProductControl.button.CornerRadius = new Avalonia.CornerRadius(7);
            ProductControl.button.Padding = new Thickness(10, 5);
            ProductControl.button.FontSize = 13;
            ProductControl.textblock.Text = "Добавить в корзину";
            Grid.SetColumn(ProductControl, 0);
            Grid.SetRow(ProductControl, 0);
            Grid.SetRowSpan(ProductControl, 2);
            GridForCardProduct.Children.Clear();
            GridForCardProduct.Children.Add(ProductControl);   
        }
    }
}
