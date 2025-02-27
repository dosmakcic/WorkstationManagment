using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using WorkstationManagment.Core.Services;

namespace WorkstationManagment.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
        }

        private readonly IServiceProvider _serviceProvider;
        

        public MainWindowViewModel(IServiceProvider serviceProvider)
        {
           
            _serviceProvider = serviceProvider;
        }
        public void NavigateToLogin()
        {
            CurrentViewModel = _serviceProvider.GetRequiredService<LoginViewModel>();
        }

    }
}
