using Avalonia.Controls;

namespace AvaloniaApplication.Views
{
    public partial class SettingsProfile : UserControl
    {
        public SettingsProfile()
        {
            InitializeComponent();
            GeneredItem();
        }

        public void GeneredItem()
        {
            //для админа
            //User user = new User();
            //Grid.SetColumn(user, 0);
            //Grid.SetRow(user, 0);
            //Grid.SetRowSpan(user, 5);
            //GridForSettingProfile.Children.Add(user);

            //для дефолт пользователя
            User user = new User();
            user.ProductsBtn.IsVisible = false;
            user.UsersBtn.IsVisible = false;
            user.GridForUser.RowDefinitions = new RowDefinitions
            {
              new RowDefinition { Height = new GridLength(10, GridUnitType.Star) },
              new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
              new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
              new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
            };
            user.LogOutBtn.SetValue(Grid.RowProperty, 3);
            Grid.SetColumn(user, 0);
            Grid.SetRow(user, 0);
            Grid.SetRowSpan(user, 4);
            GridForSettingProfile.Children.Add(user);
        }
    }
}
