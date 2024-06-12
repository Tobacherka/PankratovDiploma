using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication.Classes;
using HarfBuzzSharp;
using System.Threading.Tasks;

namespace AvaloniaApplication.Views
{
    /// <summary>
    /// Класс оформления заказа
    /// </summary>
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
            MainGrid.Children.Add(_overlayPanel);
            currentOrderId = orderId;
            DefaultFillingFields();
            btnPlaceAnOrder.Click += BtnPlaceAnOrder_Click;
        }
        
        /// <summary>
        /// обработчик нажатия на кнопку для оформления заказа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnPlaceAnOrder_Click(object? sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                if (paymentMethod == "Банковская карта")
                {
                    if (string.IsNullOrEmpty(user?.BankCardNumber))
                    {
                        await ShowCardInputDialog();
                    }
                    else
                    {
                        ShowNotificationDialog();
                    }
                }
                else
                    await FinalizeOrder();
            }
        }

        /// <summary>
        /// Метод для вывода уведомления
        /// </summary>
        private void ShowNotificationDialog()
        {
            _overlayPanel.Children.Clear();
            var maskedCardNumber = "**** **** **** " + user?.BankCardNumber?.Substring(user.BankCardNumber.Length - 4);
            var cardMessage = $"Платеж выполнен с банковской карты {maskedCardNumber}.";
            var emailMessage = $"Чек отправлен на электронную почту {user?.Email}.";
            var notification = new NotificationDialog(cardMessage, emailMessage);

            notification.OkClicked += async (s, e) =>
            {
                _overlayPanel.IsHitTestVisible = false;
                await FinalizeOrder();
            };

            _overlayPanel.Children.Add(notification);
            _overlayPanel.IsHitTestVisible = true;
        }

        /// <summary>
        /// Метод для открытия диалогового окна для ввода банковской карты
        /// </summary>
        private async Task ShowCardInputDialog()
        {
            var dialog = new CardInputDialog();
            dialog.CardNumberSubmitted += async (s, cardNumber) =>
            {
                _overlayPanel.Children.Clear();
                _overlayPanel.IsHitTestVisible = false;

                await APIWork.SendRequest("SetCardNumber", GlobalBuffer.CurrentUserID.ToString(), cardNumber);
                user = await APIWork.GetUserById(GlobalBuffer.CurrentUserID);
                ShowNotificationDialog();
            };

            dialog.Cancelled += (s, e) =>
            {
                _overlayPanel.Children.Clear();
                _overlayPanel.IsHitTestVisible = false;
            };

            _overlayPanel.Children.Add(dialog);
            _overlayPanel.IsHitTestVisible = true;
            await Task.Delay(500);
        }

        /// <summary>
        /// Метод для оформления заказа
        /// </summary>
        public async Task FinalizeOrder()
        {
            string address = $"{tbRegion.Text}, {tbCity.Text}, {tbStreetHouseApartament.Text}, {tbPostalCode.Text}";
            await APIWork.SendRequest("PlaceAnOrder", currentOrderId.ToString(), tbFullName.Text, tbPhone.Text, tbEmail.Text, paymentMethod, address, deliveryMethod);
            GlobalBuffer._mainGrid.Children.Clear();
            GlobalBuffer._mainGrid.Children.Add(new Catalog());
        }

        /// <summary>
        /// Обработчик выбора способа доставки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbDelivery_Checked(object? sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                deliveryMethod = radioButton.Content?.ToString();
            }
        }

        /// <summary>
        /// Обработчик выбора способа оплаты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbPayment_Checked(object? sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                paymentMethod = radioButton.Content?.ToString();
            }
        }

        /// <summary>
        /// Метод для проверки заполненности полей
        /// </summary>
        /// <returns>Заполненность</returns>
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

        /// <summary>
        /// Метод для заполнения данных, которые уже указаны в профиле
        /// </summary>
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
