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

        public Product()
        {
            InitializeComponent();
            button.Click += Button_Click;
        }

        public Product(Product product)
        {
            InitializeComponent();
            Id = product.Id;
            name.Text = product.Name;
            price.Text = product.price.Text;
            category.SelectedValue = product.category.SelectedValue;
            image.Source = product.image.Source;
        }

        private void Button_Click(object? sender, RoutedEventArgs e)
        {
            GlobalBuffer._mainGrid.Children.Clear();
            CardProduct cardProduct = new(new Product(this));
            GlobalBuffer._mainGrid.Children.Add(cardProduct);
        }
    }
}
