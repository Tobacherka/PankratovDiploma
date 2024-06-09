using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using AvaloniaApplication.Classes;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaApplication.Views
{
    public partial class Cart : UserControl
    {
        private int currentOrderId;

        public Cart()
        {
            InitializeComponent();
            GeneredItems();
            SetTotalCost();
            btnPlaceAnOrder.Click += BtnPlaceAnOrder_Click;
            btnEmptyTheTrash.Click += BtnEmptyTheTrash_Click;
        }

        public async Task GeneredItems()
        {
            var orderDetails = await APIWork.GetProductsInCart();
            GridForCart.Children.Clear();
            if (orderDetails != null)
            {
                int column = 0, row = 0;
                foreach (var orderDetail in orderDetails)
                {
                    if (column < 6)
                    {
                        var dbProduct = GlobalBuffer.Products.Where(x => x.Id == orderDetail.ProductID).FirstOrDefault();

                        var productControl = new Product();
                        productControl.button.IsVisible = false;
                        productControl.menuOrders.IsVisible = false;
                        productControl.buttonOrders.IsVisible = false;
                        productControl.Id = dbProduct.Id;
                        productControl.name.Text = dbProduct.Name;
                        productControl.price.Text = orderDetail.Price.ToString("F2");
                        productControl.category.Text = dbProduct.Category;
                        productControl.image.Source = ImageConverter(dbProduct.image);
                        productControl.CountInOrder = orderDetail.Quantity;
                        productControl.CurrentCart = this;
                        productControl.buttonCart.Content = $"             {productControl.CountInOrder}             ";
                        //productControl.plus.Click += ProductPlusButton_Click;
                        //productControl.minus.Click += ProductPlusButton_Click;
                        Grid.SetRow(productControl, row);
                        Grid.SetColumn(productControl, column);
                        GridForCart.Children.Add(productControl);

                        column++;
                    }
                    else
                    {
                        row++;
                        column = 0;
                    }
                }
            }

            //for (int i = 0; i < 6; i++)
            //{
            //    Product product = new Product();
            //    product.button.IsVisible = false;
            //    product.menuOrders.IsVisible = false;
            //    product.buttonOrders.IsVisible = false;
            //    Grid.SetRow(product, 0);
            //    Grid.SetColumn(product, i);
            //    GridForCart.Children.Add(product);
            //}
        }

        private Bitmap ImageConverter(byte[]? bytes)
        {
            if (bytes == null)
            {
                var uri = new Uri("avares://AvaloniaApplication/Assets/photo.png");
                using (var stream = AssetLoader.Open(uri))
                {
                    var bitmap = new Bitmap(stream);
                    return bitmap;
                }
            }
            using (MemoryStream stream = new MemoryStream(bytes))
                return new(stream);
        }

        private async void BtnEmptyTheTrash_Click(object? sender, RoutedEventArgs e)
        {
            if (GlobalBuffer.CurrentUserID > -1)
            {
                await EmptyTheTrash();
                Refresh();
                SetTotalCost();
            }
        }

        private async Task EmptyTheTrash()
        {
            try
            {
                await APIWork.SendRequest("EmptyTheTrash", GlobalBuffer.CurrentUserID.ToString());
                return;
            }
            catch
            {
                return;
            }
        }

        private async void SetTotalCost()
        {
            decimal? totalCost;
            try
            {
                var response = await APIWork.GetUserCart();
                currentOrderId = response.OrderID;
                totalCost = response.TotalCost;
            }
            catch
            {
                totalCost = 0.00m;
            }
            tbTotalCost.Text = $"Общая сумма: {(totalCost == null ? 0.00m : totalCost)}";
        }

        public async void Refresh()
        {
            var orderDetails = await APIWork.GetProductsInCart();
            GridForCart.Children.Clear();
            if (orderDetails != null)
            {
                int column = 0, row = 0;
                foreach (var orderDetail in orderDetails)
                {
                    if (column < 6)
                    {
                        var dbProduct = GlobalBuffer.Products.Where(x => x.Id == orderDetail.ProductID).FirstOrDefault();

                        var productControl = new Product();
                        productControl.button.IsVisible = false;
                        productControl.menuOrders.IsVisible = false;
                        productControl.buttonOrders.IsVisible = false;
                        productControl.Id = dbProduct.Id;
                        productControl.name.Text = dbProduct.Name;
                        productControl.price.Text = orderDetail.Price.ToString();
                        productControl.category.Text = dbProduct.Category;
                        productControl.image.Source = ImageConverter(dbProduct.image);
                        productControl.CountInOrder = orderDetail.Quantity;
                        productControl.CurrentCart = this;
                        productControl.buttonCart.Content = $"             {productControl.CountInOrder}             ";
                        //productControl.plus.Click += ProductPlusButton_Click;
                        //productControl.minus.Click += ProductPlusButton_Click;
                        Grid.SetRow(productControl, row);
                        Grid.SetColumn(productControl, column);
                        GridForCart.Children.Add(productControl);

                        column++;
                    }
                    else
                    {
                        row++;
                        column = 0;
                    }
                }
            }
        }

        private void BtnPlaceAnOrder_Click(object? sender, RoutedEventArgs e)
        {
            if (GridForCart.Children.Any())
            {
                GlobalBuffer._mainGrid.Children.Clear();
                GlobalBuffer._mainGrid.Children.Add(new PlacingAnOrder(currentOrderId));
            }
        }
    }
}
