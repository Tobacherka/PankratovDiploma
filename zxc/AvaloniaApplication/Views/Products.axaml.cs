using Avalonia.Controls;
using Avalonia.Media.Imaging;
using AvaloniaApplication.Classes;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace AvaloniaApplication.Views
{
    public partial class Products : UserControl
    {
        public Products()
        {
            InitializeComponent();
            GeneredItems();
        }

        public void GeneredItems()
        {
            //var file = await APIWork.SendRequest("SendMeAllProduct");

            //string json = await File.ReadAllTextAsync(file[0]);
            //DbData? data = JsonConvert.DeserializeObject<DbData>(json);

            //if (data == null)
            //    return;

            //int column = 0, row = 0;
            //foreach (var product in data.Products)
            //{
            //    if (column < 6)
            //    {
            //        var productControl = new Product();
            //        productControl.name.Text = product.Name;
            //        productControl.price.Text = product.Price.ToString();
            //        productControl.category.SelectedValue = product.Category;
            //        productControl.image.Source = ImageConverter(product.image);

            //        productControl.plus.IsVisible = false;
            //        productControl.minus.IsVisible = false;
            //        productControl.buttonCart.IsVisible = false;
            //        productControl.buttonOrders.IsVisible = false;
            //        productControl.menuOrders.IsVisible = false;
            //        Grid.SetRow(productControl, row);
            //        Grid.SetColumn(productControl, column);
            //        GridForProducts.Children.Add(productControl);

            //        column++;
            //    }
            //    else
            //    {
            //        row++;
            //        column = 0;
            //    }
            //}

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Product product = new Product();
                    product.plus.IsVisible = false;
                    product.minus.IsVisible = false;
                    product.buttonCart.IsVisible = false;
                    product.buttonOrders.IsVisible = false;
                    product.menuOrders.IsVisible = false;
                    Grid.SetRow(product, i);
                    Grid.SetColumn(product, j);
                    GridForProducts.Children.Add(product);
                }
            }
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
