using Avalonia.Controls;
using AvaloniaApplication.Classes;
using System.Linq;

//� ���� ������ (��������� � ������) ������ ������ ����� ����� (����� �������� �������� ��� ���������)

namespace AvaloniaApplication.Views
{
    public partial class Catalog : UserControl
    {
        public Catalog()
        {
            InitializeComponent();
            SearchBtn.IsVisible = true;
            Search.IsVisible = false;
            GeneredItems();
            SearchBtn.Click += SearchBtn_Click;
        }

        public void GeneredItems()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Product product = new Product();
                    product.plus.IsVisible = false;
                    product.minus.IsVisible = false;
                    product.buttonCart.IsVisible = false;
                    product.buttonOrders.IsVisible = false;
                    product.menuOrders.IsVisible = false;
                    Grid.SetRow(product, i);
                    Grid.SetColumn(product, j);
                    GridForCatalog.Children.Add(product);
                }
            }
        }

        private void SearchBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SearchBtn.IsVisible = false;
            Search.IsVisible = true;
        }
    }
}
