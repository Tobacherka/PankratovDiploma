using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using AvaloniaApplication.Classes;
using System.IO;
using System.Linq;

namespace AvaloniaApplication.Views
{
    public partial class Cart : UserControl
    {
        public Cart()
        {
            InitializeComponent();
            GeneredItems();
            //SetTotalCost();
        }

        public async void GeneredItems()
        {
            var orderDetails = await APIWork.GetProductsInCart();

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
                return new("D:\\учеба\\Диплом Панкратова\\Диплом\\Pankratov\\zxc\\AvaloniaApplication\\Assets\\photo.png");
            using (MemoryStream stream = new MemoryStream(bytes))
                return new(stream);
        }

        private void BtnEmpltyTheTrash_Click(object? sender, RoutedEventArgs e)
        {
            if (GlobalBuffer.CurrentUserID > -1)
            {
                EmptyTheTrash();
                //SetTotalCost();
            }
        }

        private async void EmptyTheTrash()
        {
            try
            {
                await APIWork.SendRequest("EmptyTheTrash");
            }
            catch
            {
                
            }
        }

        //private async void SetTotalCost()
        //{
        //    decimal? totalCost;
        //    try
        //    {
        //        var response = await APIWork.GetUserCart();
        //        totalCost = response.TotalCost;
        //    }
        //    catch
        //    {
        //        totalCost = 0.00m;
        //    }
        //    tbTotalCost.Text = $"Общая сумма: {totalCost}";
        //}

        //private void ProductPlusButton_Click(object? sender, RoutedEventArgs e) 
        //{
        //    SetTotalCost();
        //}
    }
}
