using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
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
        private List<Control>? _allProducts;
        public Catalog()
        {
            InitializeComponent();
            SearchBtn.IsVisible = true;
            Search.IsVisible = false;
            GeneredItems();
            SearchBtn.Click += SearchBtn_Click;
            SearchTb.TextChanged += SearchTb_TextChanged;
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
                    productControl.price.Text = product.Price.ToString() + "₽";
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
                    productControl.price.Text = product.Price.ToString() + "₽";
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
            }

            _allProducts = GridForCatalog.Children.ToList();
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

        private void SearchTb_TextChanged(object? sender, RoutedEventArgs e)
        {
            string? searchText = SearchTb.Text?.ToLower();
            SearchGridForCatalog(searchText);
        }

        private void LstBoxCategories_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedItem = e.AddedItems[0] as ListBoxItem;
                if (selectedItem != null)
                {
                    var filteredCatalog = _allProducts.Where(child =>
                    {
                        if (child is Product product)
                        {
                            if (product.category.Text.ToLower() == selectedItem.Content.ToString().ToLower())
                            {
                                return true;
                            }
                        }
                        return false;
                    }).ToList();
                    GridFilling(filteredCatalog);
                }
            }
        }

        private void SearchGridForCatalog(string? searchText)
        {
            GridForCatalog.Children.Clear();

            var filteredCatalog = _allProducts?.Where(child =>
            {
                if (child is Product product)
                {
                    if (product.category.Text?.ToLower().Contains(searchText ?? string.Empty) == true ||
                    product.name.Text?.ToLower().Contains(searchText ?? string.Empty) == true ||
                    product.price.Text?.ToLower().Contains(searchText ?? string.Empty) == true)
                    {
                        return true;
                    }
                }
                return false;
            }).ToList();
            GridFilling(filteredCatalog);
        }

        private void GridFilling(List<Control>? products)
        {
            if (products == null)
                return;

            int column = 0, row = 0;
            foreach (var product in products)
            {
                if (column < 6)
                {
                    Grid.SetRow(product, row);
                    Grid.SetColumn(product, column);
                    GridForCatalog.Children.Add(product);

                    column++;
                }
                else
                {
                    row++;
                    column = 0;
                    Grid.SetRow(product, row);
                    Grid.SetColumn(product, column);
                    GridForCatalog.Children.Add(product);
                }
            }
        }

        private void LstBoxCategoriesFilling(List<Control> categories)
        {
            
        }

    }
}
