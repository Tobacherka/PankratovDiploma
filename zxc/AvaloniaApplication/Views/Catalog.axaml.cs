using Avalonia.Controls;
using Avalonia.Media.Imaging;
using AvaloniaApplication.Classes;
using Newtonsoft.Json;
using SkiaSharp;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

//в лист боксах (категория и фильтр) должны лежать текст блоки (чтобы работала анимация при наведении)

namespace AvaloniaApplication.Views
{
    public partial class Catalog : UserControl
    {
        public Catalog()
        {
            InitializeComponent();
            SearchBtn.IsVisible = true;
            Search.IsVisible = false;
            GeneredItems();
            SearchBtn.Click += SearchBtn_Click;
        }

        private async void GeneredItems()
        {
            var file = await APIWork.SendRequest("SendMeAllProduct");
            string json = await File.ReadAllTextAsync(file[0]);

            GlobalBuffer.Products = JsonConvert.DeserializeObject<List<DbProduct>>(json);

            var products = GlobalBuffer.Products;

            if (products == null)
                return;

            int column = 0, row = 0;
            foreach (var product in products)
            {
                if (column < 6)
                {
                    var productControl = new Product(true);
                    productControl.Id = product.Id;
                    productControl.name.Text = product.Name;
                    productControl.price.Text = product.Price.ToString();
                    productControl.category.Text = product.Category;
                    productControl.image.Source = ImageConverter(product.image);

                    productControl.plus.IsVisible = false;
                    productControl.minus.IsVisible = false;
                    productControl.buttonCart.IsVisible = false;
                    productControl.buttonOrders.IsVisible = false;
                    productControl.menuOrders.IsVisible = false;
                    Grid.SetRow(productControl, row);
                    Grid.SetColumn(productControl, column);
                    GridForCatalog.Children.Add(productControl);

                    column++;
                }
                else
                {
                    row++;
                    column = 0;
                }
            }
        }

        private void SearchBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SearchBtn.IsVisible = false;
            Search.IsVisible = true;
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
