using Avalonia.Controls;
using AvaloniaApplication.Classes;

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
        }

        private void IconBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Catalog catalog = new Catalog();
            GlobalBuffer._mainGrid.Children.Clear();
            GlobalBuffer._mainGrid.Children.Add(catalog);
        }

        private void ProfileBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (GlobalBuffer.Name != null)
            {
                Profile profile = new Profile();
                GlobalBuffer._mainGrid.Children.Clear();
                GlobalBuffer._mainGrid.Children.Add(profile);
            }
            else
            {
                Authorization authorization = new Authorization();
                GlobalBuffer._mainGrid.Children.Clear();
                GlobalBuffer._mainGrid.Children.Add(authorization);
            }
        }

        private void CartBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (GlobalBuffer.Name != null)
            {
                Cart basket = new Cart();
                GlobalBuffer._mainGrid.Children.Clear();
                GlobalBuffer._mainGrid.Children.Add(basket);
            }
            else
            {
                Authorization authorization = new Authorization();
                GlobalBuffer._mainGrid.Children.Clear();
                GlobalBuffer._mainGrid.Children.Add(authorization);
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

            //Catalog catalog = new Catalog();
            //Grid.SetRow(catalog, 0);
            //MainGrid.Children.Add(catalog);

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

            //Users users = new Users();
            //Grid.SetRow(users, 0);
            //MainGrid.Children.Add(users);

            SettingUsers settingUsers = new SettingUsers();
            Grid.SetRow(settingUsers, 0);
            MainGrid.Children.Add(settingUsers);
        }
    }
}
