using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaApplication.Views
{
    public partial class NotificationDialog : Window
    {
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
            this.Close();
        }
    }
}
