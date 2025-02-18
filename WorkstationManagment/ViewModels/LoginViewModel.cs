using System;
using System.Reactive;
using System.Threading.Tasks;
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


        public LoginViewModel(IScreen screen, IAuthService authService, INavigationService navigationService)
        {
            HostScreen = screen ?? throw new ArgumentNullException(nameof(screen));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

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
                if (user.Role?.Name == "Admin")
                {
                    _navigationService.NavigateTo(Locator.Current.GetService<AdminViewModel>());
                }
                else
                {
                    var userViewModel = Locator.Current.GetService<UserViewModel>();


                    userViewModel.SetUser(user);

                    _navigationService.NavigateTo(userViewModel);
                }
            }
        }
    }


}

