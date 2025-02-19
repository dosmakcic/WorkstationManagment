using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Splat;
using WorkstationManagment.UI.ViewModels;

namespace WorkstationManagment.UI.Views
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Locator.Current.GetService<MainWindowViewModel>();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

