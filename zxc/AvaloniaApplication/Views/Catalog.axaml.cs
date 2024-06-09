using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using AvaloniaApplication.Classes;
using Newtonsoft.Json;
using SkiaSharp;
using System;
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
            //var file = await APIWork.SendRequest("SendMeAllProduct");
            //string json = await File.ReadAllTextAsync(file[0]);

            //GlobalBuffer.Products = JsonConvert.DeserializeObject<List<DbProduct>>(json);
            GlobalBuffer.Products = await APIWork.GetProducts();

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
            //try
            //{
            //    if (bytes == null)
            //    {
            //        string filePath = "Assets\\photo.png";

            //        // Ensure the file exists
            //        if (!File.Exists(filePath))
            //        {
            //            throw new FileNotFoundException("The specified file was not found.", filePath);
            //        }

            //        // Load the bitmap
            //        return new Bitmap(filePath);
            //    }
            //    using (MemoryStream stream = new MemoryStream(bytes))
            //        return new(stream);
            //}
            //catch (FileNotFoundException ex)
            //{
            //    Console.WriteLine($"File not found: {ex.Message}");
            //    // Handle the error or return a default image
            //    return null;
            //}
            //catch (UnauthorizedAccessException ex)
            //{
            //    Console.WriteLine($"Access denied: {ex.Message}");
            //    // Handle the error or return a default image
            //    return null;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"An error occurred: {ex.Message}");
            //    // Handle the error or return a default image
            //    return null;
            //}
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
    }
}
