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
    /// <summary>
    /// ����� �������
    /// </summary>
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

        /// <summary>
        /// ����� ��� ��������� �������
        /// </summary>
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

        /// <summary>
        /// ����� ��� ����������� ������� ������ � �����������
        /// </summary>
        /// <param name="bytes">������ ������</param>
        /// <returns>�����������</returns>
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

        /// <summary>
        /// ���������� ������� �� ������ ��� ������� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnEmptyTheTrash_Click(object? sender, RoutedEventArgs e)
        {
            if (GlobalBuffer.CurrentUserID > -1)
            {
                await EmptyTheTrash();
                Refresh();
                SetTotalCost();
            }
        }

        /// <summary>
        /// ����� ��� ������� �������
        /// </summary>
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

        /// <summary>
        /// ����� �������� ����� ��������� �������
        /// </summary>
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
            tbTotalCost.Text = $"����� �����: {(totalCost == null ? 0.00m : totalCost?.ToString("F2"))}";
        }

        /// <summary>
        /// ����� ��� ���������� �������
        /// </summary>
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

        /// <summary>
        /// ���������� ������� �� ������ ��� ������������ ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
