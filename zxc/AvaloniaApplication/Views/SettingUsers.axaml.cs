using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaApplication.Views
{
    public partial class SettingUsers : UserControl
    {
        public SettingUsers()
        {
            InitializeComponent();
            GeneredItem();
        }

        public void GeneredItem()
        {
            UserMin userMin = new UserMin();
            userMin.menu.IsVisible = false;
            userMin.role.IsEnabled = true;
            userMin.fio.IsVisible = false;
            userMin.button.IsVisible = false;
            userMin.border.CornerRadius = new Avalonia.CornerRadius(30);
            userMin.border.BoxShadow = new BoxShadows(
                new BoxShadow
                {
                    OffsetX = 0,
                    OffsetY = 0,
                    Blur = 25,
                    Spread = 0,
                    Color = Color.Parse("#7f000000"),

                }
            );
            userMin.gridForUserMin.Margin = new Thickness(25);
            userMin.role.FontSize = 11;
            userMin.gridForUserMin.RowDefinitions = new RowDefinitions
            {
                new RowDefinition { Height = new GridLength(10, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
            };
            Grid.SetColumn(userMin, 0);
            Grid.SetRow(userMin, 0);
            Grid.SetRowSpan(userMin, 3);
            GridForSettingUsers.Children.Add(userMin);
        }
    }
}
