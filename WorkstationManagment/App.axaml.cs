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
using WorkstationManagment.UI;

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


        Locator.CurrentMutable.Register(() => new LoginView(), typeof(IViewFor<LoginViewModel>));
        Locator.CurrentMutable.Register(() => new MainWindow(), typeof(IViewFor<MainWindowViewModel>));
        Locator.CurrentMutable.Register(() => new AdminView(), typeof(IViewFor<AdminViewModel>));
        Locator.CurrentMutable.Register(() => new UserView(), typeof(IViewFor<UserViewModel>));
       



        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        { 


            var mainWindowViewModel = ServiceProvider.GetRequiredService<MainWindowViewModel>();


            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };
            mainWindowViewModel.NavigateToLogin();

        }


        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(IServiceCollection services)
    {
       
        string connectionString = "server=localhost;port=3306;database=workstation_db;user=root;password=my-secret-pw;";
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
      
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWorkPositionService, WorkPositionService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddSingleton<RoutingState>();
        
        
        
        services.AddSingleton<AdminViewModel>();
        services.AddSingleton<UserViewModel>();
        services.AddSingleton<LoginViewModel>();
        //  services.AddSingleton<Lazy<LoginViewModel>>(sp => new Lazy<LoginViewModel>(() => sp.GetRequiredService<LoginViewModel>()));
        
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<IScreen, MainWindowViewModel>();

        services.AddTransient<AdminView>();




    }

}