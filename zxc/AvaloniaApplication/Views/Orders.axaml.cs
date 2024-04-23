using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Svg.Skia;
using System;

//написать, чтобы при навелднии на кнопку статус заказа менялся на дату

namespace AvaloniaApplication.Views
{
    public partial class Orders : UserControl
    {
        public Orders()
        {
            InitializeComponent();
            GeneredItems();
        }

        public void GeneredItems()
        {
            for (int i = 0; i < 3; i++)
            {
                Product product = new Product();
                product.button.IsVisible = false;
                product.buttonCart.IsVisible = false;
                product.plus.IsVisible = false;
                product.minus.IsVisible = false; 
                product.menu.IsVisible = false;
                Grid.SetRow(product, 0);
                Grid.SetColumn(product, i);
                GridForOrders.Children.Add(product);
            }
        }
    }
}
