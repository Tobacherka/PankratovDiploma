using Avalonia.Controls;
using AvaloniaApplication.Classes;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaApplication.Views
{
    public partial class Authorization : UserControl
    {
        public Authorization()
        {
            InitializeComponent();
            SingUpBtn.Click += SingUpBtn_Click;
            LogInBtn.Click += LogInBtn_Click;
        }

        private void LogInBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LoginText.Text) && !string.IsNullOrEmpty(PasswordText.Text))
            {
                CheckDataForAutorizaion();
            }
        }

        private async Task CheckDataForAutorizaion()
        {
            var responseArray = await APIWork.SendRequest("Authorization", LoginText.Text, PasswordText.Text);
            
            if (responseArray != null && responseArray.Any())
            {
                Profile profile = new Profile();
                GlobalBuffer._mainGrid.Children.Clear();
                GlobalBuffer._mainGrid.Children.Add(profile);
                GlobalBuffer.Name = "sad";
            }
            else
            {
                ErrorText.Text = "Ошибка входа";
            }
        }

        private void SingUpBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Registration registration = new Registration();
            GlobalBuffer._mainGrid.Children.Clear();
            GlobalBuffer._mainGrid.Children.Add(registration);
        }
    }
}
