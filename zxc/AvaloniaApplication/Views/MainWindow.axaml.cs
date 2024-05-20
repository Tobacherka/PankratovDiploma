using Avalonia.Controls;
using AvaloniaApplication.Classes;

namespace AvaloniaApplication.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        GlobalBuffer.mainWindow = this;
    }
}
