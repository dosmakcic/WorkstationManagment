using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using System;
using WorkstationManagment.UI.ViewModels;

namespace WorkstationManagment.UI.ViewModels 
{
    public class MainWindowViewModel : ViewModelBase, IScreen
    {
        public RoutingState Router { get; } = new RoutingState();
        // public string UrlPathSegment => "MainWindow";
        private readonly IServiceProvider _serviceProvider;
        public MainWindowViewModel()
        {
            var loginViewModel = Locator.Current.GetService<LoginViewModel>();


            if (loginViewModel != null)
            {
               
                Router.Navigate.Execute(loginViewModel);
            }
            
        }
    }

}

