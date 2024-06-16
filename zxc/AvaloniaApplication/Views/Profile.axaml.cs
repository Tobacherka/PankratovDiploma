using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using AvaloniaApplication.Classes;
using System.Threading.Tasks;

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
            GlobalBuffer.CurrentUserID = -1;
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
            User user = new User();
            if (GlobalBuffer.CurrentUserID == 1)
                user.ProductsBtn.IsVisible = false;
            user.UsersBtn.IsVisible = false;
            user.LogOutBtn.IsVisible = true;
            user.OrdersBtn.IsVisible = false;
            user.SettingsBtn.IsVisible = false;
            user.GridForUser.RowDefinitions = new RowDefinitions
            {
              new RowDefinition { Height = new GridLength(10, GridUnitType.Star) },
              new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
              //new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
              //new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
            };
            user.LogOutBtn.SetValue(Grid.RowProperty, 1);
            Grid.SetColumn(user, 0);
            Grid.SetRow(user, 0);
            Grid.SetRowSpan(user, 4);
            GridForProfile.Children.Add(user);
            SetUserData();

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

        private async Task SetUserData()
        {
            DbUser? dbUser = await APIWork.GetUserById(GlobalBuffer.CurrentUserID);
            tbFullName.Text = dbUser?.UserSurname + " " + dbUser?.UserName + " " + dbUser?.UserPatronymic;
            tbDateBirthday.Text = dbUser.DateBirhday.ToString();
            if (!string.IsNullOrEmpty(dbUser.Region) && !string.IsNullOrEmpty(dbUser.City) && !string.IsNullOrEmpty(dbUser.StreetHouseApartament) && !string.IsNullOrEmpty(dbUser.PostalCode))
                tbAddress.Text = $"{dbUser.Region}, {dbUser.City}, {dbUser.StreetHouseApartament}, {dbUser.PostalCode}";
            tbBankCard.Text = dbUser.BankCardNumber;
            tbPhone.Text = dbUser?.Phone;
            tbEmail.Text = dbUser?.Email;
        }

        private void ProductsBtn_Click(object? sender, RoutedEventArgs e)
        {
            Products products = new();
            GlobalBuffer._mainGrid.Children.Clear();
            GlobalBuffer._mainGrid.Children.Add(products);
        }
    }
}
