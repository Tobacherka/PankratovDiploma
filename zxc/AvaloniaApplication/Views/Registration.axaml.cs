using Avalonia.Controls;
using AvaloniaApplication.Classes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaApplication.Views
{
    public partial class Registration : UserControl
    {
        private Type _type;
        public Registration()
        {
            InitializeComponent();
        }
        public Registration(Type type)
        {
            InitializeComponent();
            LogInBtn.Click += LogInBtn_Click;
            RegistrationBtn.IsEnabled = false;
            TextBoxChanging();
            RegistrationBtn.Click += RegistrationBtn_Click;
            _type = type;
        }

        private void RegistrationBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (PasswordTextBox.Text != PasswordRepeatTextBox.Text)
            {
                ErrorText.Text = "Пароли не совпадают";
            }
            else
            {
                LoadUsersDataAsync();
            }
        }

        /// <summary>
        /// Заносим данные пользователя в базу
        /// </summary>
        private async Task LoadUsersDataAsync()
        {
            var responseArray = await APIWork.SendRequest("Registration", NameTextBox.Text, SurnameTextBox.Text, MailTextBox.Text, PasswordTextBox.Text);

            if (responseArray != null && responseArray.Any())
            {
                GlobalBuffer._mainGrid.Children.Clear();
                GlobalBuffer._mainGrid.Children.Add((UserControl)Activator.CreateInstance(_type));
                GlobalBuffer.CurrentUserID = int.Parse(responseArray[0]);
                GlobalBuffer.Name = NameTextBox.Text;
            }
            else
            {
                ErrorText.Text = "Ошибка регистрации";
            }
        }

        private void AllTextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            Check();
        }

        private void LogInBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Authorization authorization = new Authorization(_type);
            GlobalBuffer._mainGrid.Children.Clear();
            GlobalBuffer._mainGrid.Children.Add(authorization);
        }

        void Check()
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(SurnameTextBox.Text) ||
                string.IsNullOrWhiteSpace(MailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordRepeatTextBox.Text))

            {
                RegistrationBtn.IsEnabled = false;
            }
            else
            {
                RegistrationBtn.IsEnabled = true;
            }
        }

        void TextBoxChanging()
        {
            NameTextBox.TextChanged += AllTextBox_TextChanged;
            SurnameTextBox.TextChanged += AllTextBox_TextChanged;
            MailTextBox.TextChanged += AllTextBox_TextChanged;
            PasswordRepeatTextBox.TextChanged += AllTextBox_TextChanged;
            PasswordTextBox.TextChanged += AllTextBox_TextChanged;
        }
    }
}
