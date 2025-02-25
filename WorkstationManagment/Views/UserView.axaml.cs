using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using WorkstationManagment.UI.ViewModels;

namespace WorkstationManagment.UI.Views
{
    public partial class UserView : ReactiveUserControl<UserViewModel>
    {
        public UserView()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

