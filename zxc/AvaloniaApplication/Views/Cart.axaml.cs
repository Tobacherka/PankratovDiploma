using Avalonia.Controls;
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

                        Product currentProduct = new Product();
                        currentProduct.button.IsVisible = false;
                        currentProduct.menuOrders.IsVisible = false;
                        currentProduct.buttonOrders.IsVisible = false;
                        currentProduct.Id = dbProduct.Id;
                        currentProduct.name.Text = dbProduct.Name;
                        currentProduct.price.Text = dbProduct.Price.ToString();
                        currentProduct.category.Text = dbProduct.Category;
                        currentProduct.image.Source = ImageConverter(dbProduct.image);
                        currentProduct.CountInOrder = orderDetail.Quantity;
                        currentProduct.buttonCart.Content = $"             {currentProduct.CountInOrder}             ";
                        Grid.SetRow(currentProduct, row);
                        Grid.SetColumn(currentProduct, column);
                        GridForCart.Children.Add(currentProduct);

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
    }
}
