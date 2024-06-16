using Avalonia.Controls;
using AvaloniaApplication.Classes;

namespace AvaloniaApplication.Views
{
    public partial class User : UserControl
    {
        public User()
        {
            InitializeComponent();
            LogOutBtn.Click += LogOutBtn_Click;
        }

        private void LogOutBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            GlobalBuffer.CurrentUserID = -1;
            GlobalBuffer.Name = null;
            Catalog catalog = new Catalog();
            GlobalBuffer._mainGrid.Children.Clear();
            GlobalBuffer._mainGrid.Children.Add(catalog);
        }
    }
}
