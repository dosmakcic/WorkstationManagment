using ReactiveUI;
using Splat;
using System;
using System.Reactive;
using System.Threading.Tasks;
using WorkstationManagment.Core.Models;
using WorkstationManagment.Core.Services;
using WorkstationManagment.UI.ViewModels;

namespace WorkstationManagment.UI.ViewModels
{
    public class UserViewModel : ViewModelBase, IRoutableViewModel
    {
        private readonly INavigationService _navigationService;
        

        public UserViewModel(IScreen screen, IAuthService authService, INavigationService navigationService)
        {
            HostScreen = screen ?? throw new ArgumentNullException(nameof(screen));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            

            LogoutCommand = ReactiveCommand.CreateFromTask(LogoutAsync);
        }

        public string? UrlPathSegment => "user";
        public IScreen HostScreen { get; }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            private set => this.RaiseAndSetIfChanged(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            private set => this.RaiseAndSetIfChanged(ref _lastName, value);
        }

        private string _roleName;
        public string RoleName
        {
            get => _roleName;
            private set => this.RaiseAndSetIfChanged(ref _roleName, value);
        }

        private string _workPositionName;
        public string WorkPositionName
        {
            get => _workPositionName;
            private set => this.RaiseAndSetIfChanged(ref _workPositionName, value);
        }

        public ReactiveCommand<Unit, Unit> LogoutCommand { get; }

        // Metoda za postavljanje User objekta
        public void SetUser(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            RoleName = user.Role?.Name ?? "Unknown";
           // WorkPositionName = user.WorkPosition?.Name ?? "No work position assigned";
        }

        private async Task LogoutAsync()
        {
            await Task.Delay(100); // Simulacija asinhronog poziva ako je potrebno
            var loginViewModel = Locator.Current.GetService<LoginViewModel>();
            _navigationService.NavigateTo(loginViewModel);
        }
    }
}