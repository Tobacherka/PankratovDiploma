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


namespace AvaloniaApplication.Views
{
    /// <summary>
    /// Класс каталога
    /// </summary>
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

        /// <summary>
        /// Метод для генерации каталога
        /// </summary>
        private async void GeneredItems()
        {
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

        /// <summary>
        /// Метод для конвертации массива байтов в изображение
        /// </summary>
        /// <param name="bytes">Массив байтов</param>
        /// <returns>Изображение</returns>
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
    }
}
