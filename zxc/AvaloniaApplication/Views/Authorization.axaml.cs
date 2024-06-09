using Avalonia.Controls;
using AvaloniaApplication.Classes;
using DynamicData;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaApplication.Views
{
    /// <summary>
    /// Класс авторизации
    /// </summary>
    public partial class Authorization : UserControl
    {
        public Type Type { get; set; }
        public object? parametr;
        public Authorization()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Конструктор, использующийся при необходимости открыть после авторизации то окно, которое изначально хотел увидеть пользователь
        /// </summary>
        /// <param name="type">Тип окна</param>
        /// <param name="parametr">Параметры для открытия окна</param>
        public Authorization(Type type, object? parametr = null)
        {
            InitializeComponent();
            Type = type;
            this.parametr = parametr;
            SingUpBtn.Click += SingUpBtn_Click;
            LogInBtn.Click += LogInBtn_Click;
        }

        /// <summary>
        /// Обработчик нажатия на кнопку для авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogInBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LoginText.Text) && !string.IsNullOrEmpty(PasswordText.Text))
            {
                CheckDataForAuthorizaion();
            }
        }

        /// <summary>
        /// Проверка данных, введенных пользователем
        /// </summary>
        private async void CheckDataForAuthorizaion()
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

        /// <summary>
        /// Обработчик нажатия кнопки для перехода к регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingUpBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Registration registration = new Registration(Type);
            GlobalBuffer._mainGrid.Children.Clear();
            GlobalBuffer._mainGrid.Children.Add(registration);
        }
    }
}
