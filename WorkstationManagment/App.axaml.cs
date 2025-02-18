using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkstationManagment.Core.Services;
using WorkstationManagment.UI.ViewModels;
using WorkstationManagment.UI.Views;
using WorkstationManagment.Core.Data;
using Microsoft.EntityFrameworkCore;
using Splat;
using ReactiveUI;

namespace WorkstationManagment;

public partial class App : Application
{

    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {

        var services = new ServiceCollection();
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();
        Locator.CurrentMutable.InitializeReactiveUI();
        Locator.CurrentMutable.RegisterConstant(serviceProvider);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {

            var mainWindowViewModel = serviceProvider.GetRequiredService<MainWindowViewModel>();


            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        string connectionString = "server=localhost;port=3306;database=workstation_db;user=root;password=my-secret-pw;";
        // Dodaj bazu podataka (EF Core)
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWorkPositionService, WorkPositionService>();


       
        services.AddSingleton<LoginViewModel>();
        services.AddTransient<AdminViewModel>();
        services.AddTransient<UserViewModel>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<IScreen>(sp => sp.GetRequiredService<MainWindowViewModel>());






    }

}