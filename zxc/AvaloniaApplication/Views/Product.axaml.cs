using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication.Classes;
using System;
using System.Xml.Serialization;

namespace AvaloniaApplication.Views
{
    public partial class Product : UserControl
    {
        public int Id { get; set; }
        public int CountInOrder { get; set; }

        public Product()
        {
            InitializeComponent();
            button.Click += Button_Click;
        }

        public Product(Product product)
        {
            InitializeComponent();
            //button.Click -= Button_Click;
            button.Click += AddToCartButton_Click;
            plus.Click += PlusButton_Click;
            minus.Click += MinusButton_Click;
            Id = product.Id;
            name.Text = product.name.Text;
            price.Text = product.price.Text;
            category.Text = product.category.Text;
            image.Source = product.image.Source;
        }

        private void Button_Click(object? sender, RoutedEventArgs e)
        {
            GlobalBuffer._mainGrid.Children.Clear();
            CardProduct cardProduct = new(new Product(this));
            GlobalBuffer._mainGrid.Children.Add(cardProduct);
        }

        private void AddToCartButton_Click(object? sender, RoutedEventArgs e)
        {
            button.IsVisible = false;
            plus.IsVisible = true;
            minus.IsVisible = true;
            buttonCart.IsVisible = true;
            CountInOrder++;
        }

        private void PlusButton_Click(object? sender, RoutedEventArgs e)
        {
            CountInOrder++;
            buttonCart.Content = $"             {CountInOrder}             ";
        }

        private void MinusButton_Click(object? sender, RoutedEventArgs args)
        {
            CountInOrder--;

            if (CountInOrder > 0) 
            {
                buttonCart.Content = $"             {CountInOrder}             ";
                return;
            }

            button.IsVisible = true;
            plus.IsVisible = false;
            minus.IsVisible = false;
            buttonCart.IsVisible = false;
        }
    }
}
