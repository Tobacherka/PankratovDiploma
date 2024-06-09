using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaApplication.Classes;

namespace AvaloniaApplication.Views
{
    public partial class Profile : UserControl
    {
        public Profile()
        {
            InitializeComponent();
            GeneredItem();
        }

        private void LogOutBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            GlobalBuffer.Name = null;
            Catalog catalog = new Catalog();
            GlobalBuffer._mainGrid.Children.Clear();
            GlobalBuffer._mainGrid.Children.Add(catalog);
        }

        public void GeneredItem()
        {
            //для админа
            //User user = new User();
            //Grid.SetColumn(user, 0);
            //Grid.SetRow(user, 0);
            //Grid.SetRowSpan(user, 5);
            //GridForProfile.Children.Add(user);

            //для дефолт пользователя
            //User user = new User();
            //user.ProductsBtn.IsVisible = false;
            //user.UsersBtn.IsVisible = false;
            //user.GridForUser.RowDefinitions = new RowDefinitions
            //{
            //  new RowDefinition { Height = new GridLength(10, GridUnitType.Star) },
            //  new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
            //  new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
            //  new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
            //};
            //user.LogOutBtn.SetValue(Grid.RowProperty, 3);
            //Grid.SetColumn(user, 0);
            //Grid.SetRow(user, 0);
            //Grid.SetRowSpan(user, 4);
            //GridForProfile.Children.Add(user);

            //для просмотра админом
            //User user = new User();
            //user.ProductsBtn.IsVisible = false;
            //user.UsersBtn.IsVisible = false;
            //user.SettingsBtn.IsVisible = false;
            //user.LogOutBtn.IsVisible = false;
            //user.GridForUser.RowDefinitions = new RowDefinitions
            //{
            //  new RowDefinition { Height = new GridLength(10, GridUnitType.Star) },
            //  new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
            //};
            //Grid.SetColumn(user, 0);
            //Grid.SetRow(user, 0);
            //Grid.SetRowSpan(user, 3);
            //GridForProfile.Children.Add(user);
        }
    }
}
