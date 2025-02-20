using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using WorkstationManagment.Core.Services;

namespace WorkstationManagment.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen
    {
        public RoutingState Router { get; }
        private readonly IServiceProvider _serviceProvider;

        public MainWindowViewModel(RoutingState router, IServiceProvider serviceProvider)
        {
            Router = router;
            _serviceProvider = serviceProvider;
           
        }

        public void NavigateToLogin()
        {
            var loginViewModel = _serviceProvider.GetRequiredService<LoginViewModel>();

            Router.Navigate.Execute(loginViewModel).Subscribe();
        }
    }
}
