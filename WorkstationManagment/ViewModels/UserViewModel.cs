using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using System;
using System.Reactive;
using System.Threading.Tasks;
using WorkstationManagment.Core.Models;
using WorkstationManagment.Core.Services;


namespace WorkstationManagment.UI.ViewModels
{
    public class UserViewModel : ViewModelBase, IRoutableViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkPositionService _workPositionService;
        private readonly IUserService _userService;
        private User _user;
        
        public UserViewModel(IScreen screen, IAuthService authService, INavigationService navigationService, IServiceProvider serviceProvider, IWorkPositionService workPositionService,IUserService userService)
        {
            HostScreen = screen ?? throw new ArgumentNullException(nameof(screen));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _serviceProvider = serviceProvider;
            _workPositionService = workPositionService;
            _userService = userService;

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

        private string _workPositionProductName;
        public string WorkPositionProductName
        {
            get => _workPositionProductName;
            private set => this.RaiseAndSetIfChanged(ref _workPositionProductName, value);
        }

        private string _assignedDate;
        public string AssignedDate
        {
            get => _assignedDate;
            private set => this.RaiseAndSetIfChanged(ref _assignedDate, value);
        }


        public ReactiveCommand<Unit, Unit> LogoutCommand { get; }

        
        public async void SetUser(User user)
        {
            _user = user;
            FirstName = _user.FirstName;
            LastName = _user.LastName;
            RoleName = _user.Role?.Name ?? "No role assigned";

            var user1 = await _userService.GetUserByUsernameAsync(user.Username);
            var userWorkPosition = await _workPositionService.FindUserWorkPositionByIdAsync(user1.Id);
            if (userWorkPosition != null)
            {
                WorkPositionName = userWorkPosition.WorkPosition?.Name ?? "N/A";
                WorkPositionProductName = userWorkPosition.ProductName ?? "N/A";
                AssignedDate = userWorkPosition.Date.ToString("dd.MM.yyyy") ?? "N/A";
            }
            else
            {
                WorkPositionName = "No work position assigned";
                WorkPositionProductName = "No product assigned";
                AssignedDate = "N/A";
            }


        }

        private async Task LogoutAsync()
        {
            await Task.Delay(100); 
            var loginViewModel = _serviceProvider.GetRequiredService<LoginViewModel>();
            _navigationService.NavigateTo(loginViewModel);
        }
    }
}