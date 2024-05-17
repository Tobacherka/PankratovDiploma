using Avalonia.Controls;
using AvaloniaApplication.Classes;
using DynamicData;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaApplication.Views
{
    public partial class Authorization : UserControl
    {
        public Type Type { get; set; }
        public object? parametr;
        public Authorization()
        {
            InitializeComponent();
        }
        public Authorization(Type type, object? parametr = null)
        {
            InitializeComponent();
            Type = type;
            this.parametr = parametr;
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

        private async void CheckDataForAutorizaion()
        {
            var responseArray = await APIWork.SendRequest("Authorization", LoginText.Text, PasswordText.Text);
            
            if (responseArray != null && responseArray.Any())
            {
                GlobalBuffer._mainGrid.Children.Clear();
                GlobalBuffer.CurrentUserID = int.Parse(responseArray[0]);
                GlobalBuffer.Name = "sad";
                if (parametr != null)
                    GlobalBuffer._mainGrid.Children.Add((UserControl)Activator.CreateInstance(Type, parametr));
                else
                    GlobalBuffer._mainGrid.Children.Add((UserControl)Activator.CreateInstance(Type));
                GlobalBuffer._mainGrid.Children.Remove(this);
            }
            else
            {
                ErrorText.Text = "Ошибка входа";
            }
        }

        private void SingUpBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Registration registration = new Registration(Type);
            GlobalBuffer._mainGrid.Children.Clear();
            GlobalBuffer._mainGrid.Children.Add(registration);
        }
    }
}
