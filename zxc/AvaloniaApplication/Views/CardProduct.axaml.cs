using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using AvaloniaApplication.Classes;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AvaloniaApplication.Views
{
    /// <summary>
    /// Класс карточки товара
    /// </summary>
    public partial class CardProduct : UserControl
    {
        public Product ProductControl { get; set; }
        public CardProduct()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Конструтор, использующийся для создания карточки товара, выбранного на главной странице
        /// </summary>
        /// <param name="product">Товар</param>
        public CardProduct(Product product)
        {
            ProductControl = new(product);
            InitializeComponent();
            GeneredItem();
            ChangeBtn1.Click += ChangeBtn1_Click;
            ChangeBtn2.Click += ChangeBtn2_Click;
            ChangeBtn3.Click += ChangeBtn3_Click;
        }

        private void ChangeBtn1_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (textbox1.IsEnabled == false)
                textbox1.IsEnabled = true;
            else textbox1.IsEnabled = false;
        }
        private void ChangeBtn2_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (textbox2.IsEnabled == false)
                textbox2.IsEnabled = true;
            else textbox2.IsEnabled = false;
        }
        private void ChangeBtn3_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (textbox3.IsEnabled == false)
                textbox3.IsEnabled = true;
            else textbox3.IsEnabled = false;
        }

        /// <summary>
        /// Метод для заполнения всех полей карточки товара
        /// </summary>
        public void GeneredItem()
        {
            ProductControl.buttonOrders.IsVisible = false;
            ProductControl.menuOrders.IsVisible = false;
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
            GridForCardProduct.Children.Add(ProductControl);
            FillingTheCharacteristics();
            spReviews.IsVisible = false;
        }

        /// <summary>
        /// Метод для заполнения характеристик товара
        /// </summary>
        public async void FillingTheCharacteristics()
        {
            var product = await APIWork.GetProductById(ProductControl.Id);

            if (!string.IsNullOrEmpty(product?.Description))
            {
                tbDescription.Text = "• Описание: " + product?.Description;
            }

            if (product?.Weight != null)
            {
                tbWeight.Text = "• Вес: " + product?.Weight.ToString();
            }

            if (!string.IsNullOrEmpty(product?.Materials))
            {
                tbMaterials.Text = "• Материалы: " + product?.Materials;
            }

            if (!string.IsNullOrEmpty(product?.Color))
            {
                tbColor.Text = "• Цвет: " + product?.Color;
            }

            if (!string.IsNullOrEmpty(product?.Warranty))
            {
                tbWarranty.Text = "• Гарантия: " + product?.Warranty;
            }
        }
    }
}
