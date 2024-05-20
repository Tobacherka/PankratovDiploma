using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication.Classes;

namespace AvaloniaApplication.Views
{
    public partial class PlacingAnOrder : UserControl
    {
        private int currentOrderId;
        private string? deliveryMethod;
        private string? paymentMethod;


        public PlacingAnOrder() 
        {
            InitializeComponent();
        }
        public PlacingAnOrder(int orderId)
        {
            InitializeComponent();
            currentOrderId = orderId;
            btnPlaceAnOrder.Click += BtnPlaceAnOrder_Click;
        }
        
        private async void BtnPlaceAnOrder_Click(object? sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
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
    }
}
