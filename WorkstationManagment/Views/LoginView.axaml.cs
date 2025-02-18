using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Splat;
using WorkstationManagment.UI.ViewModels;

namespace WorkstationManagment.UI.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = Locator.Current.GetService<LoginViewModel>();  // Postavljamo DataContext na LoginViewModel
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }

}

