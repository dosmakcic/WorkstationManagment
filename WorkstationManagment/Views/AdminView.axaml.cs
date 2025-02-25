using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkstationManagment.Core.Models;
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