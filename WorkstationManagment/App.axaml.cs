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
using ReactiveUI;


namespace WorkstationManagment;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; }
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {

        var services = new ServiceCollection();
        ConfigureServices(services);

        ServiceProvider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        { 
            var mainWindowViewModel = ServiceProvider.GetRequiredService<MainWindowViewModel>();
            var viewLocator = ServiceProvider.GetRequiredService<ViewLocator>();
            DataTemplates.Add(viewLocator);
            desktop.MainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            desktop.MainWindow.DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.NavigateToLogin();
        }
        

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        string connectionString = "server=localhost;port=3306;database=workstation_db;user=root;password=my-secret-pw;";
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserWorkPositionService, UserWorkPositionService>();
        services.AddScoped<IWorkPositionService, WorkPositionService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddSingleton<AdminViewModel>();
        services.AddSingleton<UserViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<ViewLocator>();
        services.AddTransient<AdminView>();
        services.AddTransient<UserView>();
        services.AddTransient<LoginView>();
        services.AddTransient<MainWindow>();   
    }

}