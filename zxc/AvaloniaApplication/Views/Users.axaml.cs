using Avalonia.Controls;

namespace AvaloniaApplication.Views
{
    public partial class Users : UserControl
    {
        public Users()
        {
            InitializeComponent();
            GeneredItems();
        }

        public void GeneredItems()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    UserMin userMin = new UserMin();
                    Grid.SetRow(userMin, i);
                    Grid.SetColumn(userMin, j);
                    GridForUsers.Children.Add(userMin);
                }
            }
        }
    }
}
