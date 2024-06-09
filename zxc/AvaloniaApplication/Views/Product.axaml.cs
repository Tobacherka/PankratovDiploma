using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AvaloniaApplication.Views
{
    public partial class Product : UserControl
    {
        public int Id { get; set; }
        public int CountInOrder { get; set; }
        public Cart? CurrentCart;

        public Product()
        {
            InitializeComponent();
            ProductSettings(this);
        }

        public Product(bool isCatalog)
        {
            InitializeComponent();
            button.Click += Button_Click;
            plus.Click += PlusButton_Click;
            minus.Click += MinusButton_Click;
        }

        public Product(Product product)
        {
            InitializeComponent();
            ProductSettings(product);
        }

        private void Button_Click(object? sender, RoutedEventArgs e)
        {
            GlobalBuffer._mainGrid.Children.Clear();
            CardProduct cardProduct = new(new Product(this));
            GlobalBuffer._mainGrid.Children.Add(cardProduct);
        }

        private void AddToCartButton_Click(object? sender, RoutedEventArgs e)
        {
            if (GlobalBuffer.CurrentUserID < 0)
            {
                GlobalBuffer._mainGrid.Children.Clear();
                Authorization authorization = new Authorization(typeof(CardProduct), new Product(this));
                GlobalBuffer._mainGrid.Children.Add(authorization);
            }
            else
            {
                button.IsVisible = false;
                plus.IsVisible = true;
                minus.IsVisible = true;
                buttonCart.IsVisible = true;
                CountInOrder++;
                AddToCart(true);
            }
        }

        private void PlusButton_Click(object? sender, RoutedEventArgs e)
        {
            CountInOrder++;
            AddToCart(false);
            buttonCart.Content = $"             {CountInOrder}             ";
        }

        private async void MinusButton_Click(object? sender, RoutedEventArgs args)
        {
            CountInOrder--;
            await AddToCart(false);

            if (CountInOrder > 0) 
            {
                buttonCart.Content = $"             {CountInOrder}             ";
                return;
            }

            if (CurrentCart != null) 
            {
                await CurrentCart.GeneredItems();
            }
            else
            {
                button.IsVisible = true;
                plus.IsVisible = false;
                minus.IsVisible = false;
                buttonCart.IsVisible = false;
            }
        }

        public async Task AddToCart(bool isNew)
        {
            if (isNew)
                await APIWork.SendRequest("AddProductToCart", GlobalBuffer.CurrentUserID.ToString(), Id.ToString());
            else
                await APIWork.SendRequest("AddProductToCart", GlobalBuffer.CurrentUserID.ToString(), Id.ToString(), CountInOrder.ToString());
            try
            {
                decimal? totalCost = 0.00m;
                var response = await APIWork.GetUserCart();
                totalCost = response.TotalCost;
                if (CurrentCart != null)
                    CurrentCart.tbTotalCost.Text = $"Общая сумма: {(totalCost == null ? 0.00m : totalCost)}";
            }
            catch
            {
               // CurrentCart.tbTotalCost.Text = string.Empty;
            }
        }

        //public async Task<DbOrderDetail?>? GetOrderDetail()
        //{
        //    var response = await APIWork.GetProductsInCart();
        //    return response?.Where(x => x?.ProductID == Id).FirstOrDefault();
        //}

        public async void ProductSettings(Product product)
        {
            Id = product.Id;
            name.Text = product.name.Text;
            price.Text = product.price.Text;
            category.Text = product.category.Text;
            image.Source = product.image.Source;
            var response = await APIWork.GetProductsInCart();
            var currenOrderDetail = response?.Where(x => x?.ProductID == Id).FirstOrDefault();
            if (currenOrderDetail != null && GlobalBuffer.CurrentUserID > -1)
            {
                CountInOrder = currenOrderDetail.Quantity;
                button.IsVisible = false;
                plus.IsVisible = true;
                minus.IsVisible = true;
                buttonCart.IsVisible = true;
                buttonCart.Content = $"             {CountInOrder}             ";
            }
            else
            {
                button.IsVisible = true;
                plus.IsVisible = false;
                minus.IsVisible = false;
                buttonCart.IsVisible = false;
            }
            if (textblock.Text == "Добавить в корзину")
                button.Click += AddToCartButton_Click;
            else
            {
                button.Click += Button_Click;
            }
            plus.Click += PlusButton_Click;
            minus.Click += MinusButton_Click;
        }
    }
}
