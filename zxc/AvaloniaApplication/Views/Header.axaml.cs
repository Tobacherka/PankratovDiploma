using Avalonia.Controls;
using AvaloniaApplication.Classes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AvaloniaApplication.Views
{
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
            CartBtn.IsVisible = true;
            ProfileBtn.IsVisible = true;
            ChatBtn.IsVisible = true;
            NotificationsBtn.IsVisible = true;
            IconBtn.Click += IconBtn_Click;
            CartBtn.Click += CartBtn_Click;
            ProfileBtn.Click += ProfileBtn_Click;
            GlobalBuffer._mainGrid = MainGrid;
            Add();
            //GlobalBuffer.Products = GetProducts().Result;
        }

        private void IconBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Catalog catalog = new Catalog();
            GlobalBuffer._mainGrid.Children.Clear();
            GlobalBuffer._mainGrid.Children.Add(catalog);
        }

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

        public void Add()
        {
            //Authorization authorization = new Authorization();
            //Grid.SetRow(authorization, 0);
            //MainGrid.Children.Add(authorization);
            //CartBtn.IsVisible = false;
            //ProfileBtn.IsVisible = false;
            //ChatBtn.IsVisible = false;
            //NotificationsBtn.IsVisible = false;

            //Registration registration = new Registration();
            //Grid.SetRow(registration, 0);
            //MainGrid.Children.Add(registration);
            //CartBtn.IsVisible = false;
            //ProfileBtn.IsVisible = false;
            //ChatBtn.IsVisible = false;
            //NotificationsBtn.IsVisible = false;

            //PasswordRecovery passwordRecovery = new PasswordRecovery();
            //Grid.SetRow(passwordRecovery, 0);
            //MainGrid.Children.Add(passwordRecovery);
            //CartBtn.IsVisible = false;
            //ProfileBtn.IsVisible = false;
            //ChatBtn.IsVisible = false;
            //NotificationsBtn.IsVisible = false;

            Catalog catalog = new Catalog();
            Grid.SetRow(catalog, 0);
            MainGrid.Children.Add(catalog);

            //CardProduct cardproduct = new CardProduct();
            //Grid.SetRow(cardproduct, 0);
            //MainGrid.Children.Add(cardproduct);

            //Cart cart = new Cart();
            //Grid.SetRow(cart, 0);
            //MainGrid.Children.Add(cart);

            //Profile profile = new Profile();
            //Grid.SetRow(profile, 0);
            //MainGrid.Children.Add(profile);

            //SettingsProfile settingsProfile = new SettingsProfile();
            //Grid.SetRow(settingsProfile, 0);
            //MainGrid.Children.Add(settingsProfile);

            //Orders orders = new Orders();
            //Grid.SetRow(orders, 0);
            //MainGrid.Children.Add(orders);

            //PlacingAnOrder placingAnOrder = new PlacingAnOrder();
            //Grid.SetRow(placingAnOrder, 0);
            //MainGrid.Children.Add(placingAnOrder);

            //Chat chat = new Chat();
            //Grid.SetRow(chat, 0);
            //MainGrid.Children.Add(chat);

            //Notifications notifications = new Notifications();
            //Grid.SetRow(notifications, 0);
            //MainGrid.Children.Add(notifications);

            //SettingsProduct settingsProduct = new SettingsProduct();
            //Grid.SetRow(settingsProduct, 0);
            //MainGrid.Children.Add(settingsProduct);

            //Products products = new Products();
            //Grid.SetRow(products, 0);
            //MainGrid.Children.Add(products);

            //Product product = new Product();
            //Grid.SetRow(product, 0);
            //MainGrid.Children.Add(product);

            //Users users = new Users();
            //Grid.SetRow(users, 0);
            //MainGrid.Children.Add(users);

            //SettingUsers settingUsers = new SettingUsers();
            //Grid.SetRow(settingUsers, 0);
            //MainGrid.Children.Add(settingUsers);
        }
        private async Task<List<DbProduct>?> GetProducts()
        {
            var file = await APIWork.SendRequest("SendMeAllProduct");
            string json = await File.ReadAllTextAsync(file[0]);
            return JsonConvert.DeserializeObject<List<DbProduct>>(json);
        }
    }
}
