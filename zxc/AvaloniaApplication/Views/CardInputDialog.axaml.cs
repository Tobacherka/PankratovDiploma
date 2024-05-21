using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Dynamic;

namespace AvaloniaApplication.Views
{
    public partial class CardInputDialog : UserControl
    {
        public event EventHandler<string?> CardNumberSubmitted;
        public event EventHandler Cancelled;
        public CardInputDialog()
        {
            InitializeComponent();
            OkButton.Click += OkButton_Click;
            CancelButton.Click += CancelButton_Click;
        }

        private void OkButton_Click(object? sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                // Обработка введённых данных
                CardNumberSubmitted?.Invoke(this, CardNumberTextBox.Text ?? null);
                //this.Close(CardNumberTextBox.Text);
            }
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
            //this.Close(null);
        }

        private bool ValidateInput()
        {
            // Простая валидация полей
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(CardNumberTextBox.Text) || CardNumberTextBox.Text.Length != 16)
            {
                isValid = false;
                CardNumberTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                CardNumberTextBox.BorderBrush = Brushes.Gray;
            }

            if (string.IsNullOrWhiteSpace(ExpiryDateTextBox.Text) || !ExpiryDateTextBox.Text.Contains("/"))
            {
                isValid = false;
                ExpiryDateTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                ExpiryDateTextBox.BorderBrush = Brushes.Gray;
            }

            if (string.IsNullOrWhiteSpace(CVVTextBox.Text) || CVVTextBox.Text.Length != 3)
            {
                isValid = false;
                CVVTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                CVVTextBox.BorderBrush = Brushes.Gray;
            }

            return isValid;
        }
    }
}
