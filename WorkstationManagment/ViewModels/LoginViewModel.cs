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
    public class LoginViewModel : ViewModelBase, IRoutableViewModel
    {
        private readonly IAuthService _authService;
        private readonly INavigationService _navigationService;
        private readonly IServiceProvider _serviceProvider;



        public LoginViewModel(IScreen screen, IAuthService authService, INavigationService navigationService, IServiceProvider serviceProvider)
        {
            HostScreen = screen ?? throw new ArgumentNullException(nameof(screen));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _serviceProvider = serviceProvider;

            LoginCommand = ReactiveCommand.CreateFromTask(ExecuteLoginAsync);
        }


        public ReactiveCommand<Unit, Unit> LoginCommand { get; }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string? UrlPathSegment => "login";
        public IScreen HostScreen { get; }

        private async Task ExecuteLoginAsync()
        {
            var user = await _authService.AuthenticateAsync(Username, Password);

            if (user != null)
            {
                if (user.RoleId == 1)
                {
                    var adminViewModel = _serviceProvider.GetRequiredService<AdminViewModel>();
                    _navigationService.NavigateTo(adminViewModel);
                }
                else
                {
                    var userViewModel = App.ServiceProvider.GetRequiredService<UserViewModel>();


                    userViewModel.SetUser(user);

                    _navigationService.NavigateTo(userViewModel);
                }
            }
        }
    }


}

