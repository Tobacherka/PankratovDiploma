using Avalonia.Controls;
using AvaloniaApplication.Classes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AvaloniaApplication.Views
{
    /// <summary>
    /// Класс для кнопок верхней панели
    /// </summary>
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
            CartBtn.IsVisible = true;
            ProfileBtn.IsVisible = true;
            //ChatBtn.IsVisible = true;
            //NotificationsBtn.IsVisible = true;
            IconBtn.Click += IconBtn_Click;
            CartBtn.Click += CartBtn_Click;
            ProfileBtn.Click += ProfileBtn_Click;
            GlobalBuffer._mainGrid = MainGrid;
            Add();
        }

        /// <summary>
        /// Обработчик нажатия на логотип
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Catalog catalog = new Catalog();
            GlobalBuffer._mainGrid.Children.Clear();
            GlobalBuffer._mainGrid.Children.Add(catalog);
        }

        /// <summary>
        /// Обработчик нажатия на профиль
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProfileBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (GlobalBuffer.Name == null)
            {
                GlobalBuffer._mainGrid.Children.Clear();
                Authorization authorization = new Authorization(typeof(Profile));
                GlobalBuffer._mainGrid.Children.Add(authorization);
            }
            else
            {
                GlobalBuffer._mainGrid.Children.Clear();
                Profile profile = new Profile();
                GlobalBuffer._mainGrid.Children.Add(profile);
            }
        }

        /// <summary>
        /// Обработчик нажатия на корзину
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CartBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (GlobalBuffer.Name == null)
            {
                GlobalBuffer._mainGrid.Children.Clear();
                Authorization authorization = new Authorization(typeof(Cart));
                GlobalBuffer._mainGrid.Children.Add(authorization);
            }
            else
            {
                GlobalBuffer._mainGrid.Children.Clear();
                Cart basket = new Cart();
                GlobalBuffer._mainGrid.Children.Add(basket);
            }
        }

        /// <summary>
        /// Метод для открытия окна
        /// </summary>
        public void Add()
        {
            Catalog catalog = new Catalog();
            Grid.SetRow(catalog, 0);
            MainGrid.Children.Add(catalog);
        }

        private async Task<List<DbProduct>?> GetProducts()
        {
            var file = await APIWork.SendRequest("SendMeAllProduct");
            string json = await File.ReadAllTextAsync(file[0]);
            return JsonConvert.DeserializeObject<List<DbProduct>>(json);
        }
    }
}
