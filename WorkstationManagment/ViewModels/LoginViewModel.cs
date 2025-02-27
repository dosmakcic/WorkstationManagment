using System;
using System.Reactive;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using WorkstationManagment.Core.Services;
using WorkstationManagment.UI.ViewModels;



namespace WorkstationManagment.UI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;
        private readonly IServiceProvider _serviceProvider;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public LoginViewModel(IAuthService authService, IServiceProvider serviceProvider, MainWindowViewModel mainWindowViewModel)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _serviceProvider = serviceProvider;
            _mainWindowViewModel = mainWindowViewModel;
            LoginCommand = ReactiveCommand.CreateFromTask(ExecuteLoginAsync);
        }


        public ReactiveCommand<Unit, Unit> LoginCommand { get; }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

       

        private async Task ExecuteLoginAsync()
        {
            var user = await _authService.AuthenticateAsync(Username, Password);

            if (user != null)
            {
                if (user.RoleId == 1)
                {
                    var adminViewModel = _serviceProvider.GetRequiredService<AdminViewModel>();
                    _mainWindowViewModel.CurrentViewModel = adminViewModel;
                }
                else
                {
                    var userViewModel = _serviceProvider.GetRequiredService<UserViewModel>();


                    userViewModel.SetUser(user);

                    _mainWindowViewModel.CurrentViewModel = userViewModel;
                }
            }
        }
    }


}

