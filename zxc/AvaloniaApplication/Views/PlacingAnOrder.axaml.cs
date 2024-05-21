using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication.Classes;
using HarfBuzzSharp;

namespace AvaloniaApplication.Views
{
    public partial class PlacingAnOrder : UserControl
    {
        private Panel _overlayPanel;
        private int currentOrderId;
        private string? deliveryMethod;
        private string? paymentMethod;
        private DbUser? user;

        public PlacingAnOrder() 
        {
            InitializeComponent();
        }
        public PlacingAnOrder(int orderId)
        {
            InitializeComponent();
            _overlayPanel = new Panel();
            MainGrid.Children.Add( _overlayPanel );
            currentOrderId = orderId;
            btnPlaceAnOrder.Click += BtnPlaceAnOrder_Click;
            DefaultFillingFields();
        }
        
        private async void BtnPlaceAnOrder_Click(object? sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                if (paymentMethod == "Банковская карта")
                {
                    if (string.IsNullOrEmpty(user?.BankCardNumber))
                    {
                        var dialog = new CardInputDialog();
                        string? result = null;
                        //var result = await dialog.ShowDialog<string>(GlobalBuffer.mainWindow);
                        dialog.CardNumberSubmitted += (s, cardNumber) =>
                        {
                            _overlayPanel.Children.Clear();
                            _overlayPanel.IsHitTestVisible = false;
                            result = cardNumber;
                            //await notification.ShowDialog(GlobalBuffer.mainWindow);
                        };

                        if (result != null)
                        {
                            await APIWork.SendRequest("SetCardNumber", GlobalBuffer.CurrentUserID.ToString(), result);
                            user = await APIWork.GetUserById(GlobalBuffer.CurrentUserID);
                        }

                        var maskedCardNumber = "**** **** **** " + user?.BankCardNumber[(user.BankCardNumber.Length - 4)..];
                        var cardMessage = $"Платеж выполнен с банковской карты {maskedCardNumber}.";
                        var emailMessage = $"Чек отправлен на электронную почту {user.Email}.";
                        var notification = new NotificationDialog(cardMessage, emailMessage);

                        notification.OkClicked += (s, e) =>
                        {
                            _overlayPanel.Children.Clear();
                            _overlayPanel.IsHitTestVisible = false;
                        };

                        _overlayPanel.Children.Add(dialog);
                        _overlayPanel.IsHitTestVisible = true;
                    }
                }
                string address = $"{tbRegion.Text}, {tbCity.Text}, {tbStreetHouseApartament.Text}, {tbPostalCode.Text}";
                await APIWork.SendRequest("PlaceAnOrder", currentOrderId.ToString(), tbFullName.Text, tbPhone.Text, tbEmail.Text, paymentMethod, address, deliveryMethod);
                GlobalBuffer._mainGrid.Children.Clear();
                GlobalBuffer._mainGrid.Children.Add(new Catalog());
            }
        }

        private void RbDelivery_Checked(object? sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                deliveryMethod = radioButton.Content?.ToString();
            }
        }

        private void RbPayment_Checked(object? sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                paymentMethod = radioButton.Content?.ToString();
            }
        }

        public bool CheckFields()
        {
            return !string.IsNullOrEmpty(tbFullName.Text)
                && !string.IsNullOrEmpty(tbEmail.Text)
                && !string.IsNullOrEmpty(tbPhone.Text)
                && !string.IsNullOrEmpty(tbRegion.Text)
                && !string.IsNullOrEmpty(tbCity.Text)
                && !string.IsNullOrEmpty(tbStreetHouseApartament.Text)
                && !string.IsNullOrEmpty(tbPostalCode.Text)
                && !string.IsNullOrEmpty(deliveryMethod)
                && !string.IsNullOrEmpty(paymentMethod);
        }

        public async void DefaultFillingFields()
        {
            user = await APIWork.GetUserById(GlobalBuffer.CurrentUserID);
            if (user != null)
            {
                var fullName = $"{(string.IsNullOrEmpty(user.UserSurname) ? string.Empty : user.UserSurname + " ")}" +
                    $"{(string.IsNullOrEmpty(user.UserName) ? string.Empty : user.UserName + " ")}" +
                    $"{(string.IsNullOrEmpty(user.UserPatronymic) ? string.Empty : user.UserPatronymic + " ")}";
                tbFullName.Text = fullName;

                var phone = user.Phone;
                tbPhone.Text = phone;

                var email = user.Email;
                tbEmail.Text = email;

                var region = user.Region;
                tbRegion.Text = region;

                var city = user.City;
                tbCity.Text = city;

                var streetHouseApartament = user.StreetHouseApartament;
                tbStreetHouseApartament.Text = streetHouseApartament;

                var postalCode = user.PostalCode;
                tbPostalCode.Text = postalCode;
            }
        }
    }
}
