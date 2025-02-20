using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using Splat;
using WorkstationManagment.UI.ViewModels;

namespace WorkstationManagment.UI.Views
{
    public partial class LoginView : ReactiveUserControl<LoginViewModel>
    {
        public LoginView()
        {
            InitializeComponent();
         //   DataContext = App.ServiceProvider.GetRequiredService<LoginViewModel>(); // Postavljamo DataContext na LoginViewModel
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }

}

