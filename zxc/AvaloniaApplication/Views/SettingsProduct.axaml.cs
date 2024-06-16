using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.IO;
using System;
using AvaloniaApplication.Classes;
using Avalonia.Interactivity;
using System.Net.Mail;

namespace AvaloniaApplication.Views
{
    public partial class SettingsProduct : UserControl
    {
        Panel _overlayPanel;
        public Product ProductControl { get; set; }
        public DbProduct? Product { get; set; }

        public SettingsProduct()
        {
            InitializeComponent();
           // GeneredItem();
        }

        public SettingsProduct(Product product)
        {
            ProductControl = new(product);
            _overlayPanel = new Panel();
            InitializeComponent();
            GeneredItem();
            btnChange.Click += BtnChange_Click;
        }

        public void GeneredItem()
        {
            ProductControl.plus.IsVisible = false;
            ProductControl.minus.IsVisible = false;
            ProductControl.buttonCart.IsVisible = false;
            ProductControl.buttonOrders.IsVisible = false;
            ProductControl.menuOrders.IsVisible = false;
            ProductControl.category.IsEnabled = true;
            ProductControl.name.IsEnabled = true;
            ProductControl.price.IsEnabled = true;
            ProductControl.border.CornerRadius = new Avalonia.CornerRadius(30);
            ProductControl.border.BoxShadow = new BoxShadows(
                new BoxShadow
                {
                    OffsetX = 0,
                    OffsetY = 0,
                    Blur = 25,
                    Spread = 0,
                    Color = Color.Parse("#7f000000"),

                }
            );
            ProductControl.grid.Margin = new Thickness(25);
            ProductControl.category.FontSize = 11;
            ProductControl.name.FontSize = 13;
            ProductControl.price.FontSize = 12;
            ProductControl.button.CornerRadius = new Avalonia.CornerRadius(7);
            ProductControl.button.Padding = new Thickness(10, 5);
            ProductControl.button.FontSize = 13;
            ProductControl.textblock.Text = "Добавить в корзину";
            Grid.SetColumn(ProductControl, 0);
            Grid.SetRow(ProductControl, 0);
            Grid.SetRowSpan(ProductControl, 2);
            GridForSettingProduct.Children.Add(ProductControl);
            FillingTheCharacteristics();
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

        public async void FillingTheCharacteristics()
        {
            Product = await APIWork.GetProductById(ProductControl.Id);

            if (!string.IsNullOrEmpty(Product?.Description))
            {
                tbDescription.Text = "• Описание: " + Product?.Description;
            }

            if (Product?.Weight != null)
            {
                tbWeight.Text = "• Вес: " + Product?.Weight.ToString();
            }

            if (!string.IsNullOrEmpty(Product?.Materials))
            {
                tbMaterials.Text = "• Материалы: " + Product?.Materials;
            }

            if (!string.IsNullOrEmpty(Product?.Color))
            {
                tbColor.Text = "• Цвет: " + Product?.Color;
            }

            if (!string.IsNullOrEmpty(Product?.Warranty))
            {
                tbWarranty.Text = "• Гарантия: " + Product?.Warranty;
            }
        }

        private async void BtnChange_Click(object? sender, RoutedEventArgs e)
        {
            decimal newPrice = decimal.Parse(ProductControl.price.Text.Replace("₽", ""));
            if (Product.Price != newPrice) 
            {
                await APIWork.SendRequest("ChangeProduct", ProductControl.Id.ToString(), newPrice.ToString());
                GlobalBuffer._mainGrid.Children.Add(_overlayPanel);
                var notification = new NotificationDialog(string.Empty, "Изменения успешно сохранены.");

                notification.OkClicked += (s, e) =>
                {
                    _overlayPanel.IsHitTestVisible = false;
                    GlobalBuffer._mainGrid.Children.Remove(_overlayPanel);
                };

                _overlayPanel.Children.Add(notification);
                _overlayPanel.IsHitTestVisible = true;
            }
        }
    }
}
