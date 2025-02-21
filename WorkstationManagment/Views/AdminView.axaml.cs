using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using WorkstationManagment.UI.ViewModels;

namespace WorkstationManagment.UI.Views;

public partial class AdminView : ReactiveUserControl<AdminViewModel>
{
    public AdminView()
    {
        InitializeComponent();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}