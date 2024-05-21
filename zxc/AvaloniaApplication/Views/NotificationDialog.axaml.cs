using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace AvaloniaApplication.Views
{
    public partial class NotificationDialog : UserControl
    {
        public event EventHandler OkClicked;
        public NotificationDialog()
        {
            InitializeComponent();
            OkButton.Click += OkButton_Click;
        }
        public NotificationDialog(string cardMessage, string emailMessage)
        {
            InitializeComponent();
            CardMessage.Text = cardMessage;
            EmailMessage.Text = emailMessage;
            OkButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object? sender, RoutedEventArgs e)
        {
            OkClicked?.Invoke(this, EventArgs.Empty);
            //this.Close();
        }
    }
}
