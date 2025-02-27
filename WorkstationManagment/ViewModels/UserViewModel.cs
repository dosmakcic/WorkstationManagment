using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive;
using System.Threading.Tasks;
using WorkstationManagment.Core.Models;
using WorkstationManagment.Core.Services;


namespace WorkstationManagment.UI.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserWorkPositionService _userWorkPositionService;
        private readonly IUserService _userService;
        private readonly IWorkPositionService _workPositionService;
        private User _user;
        private MainWindowViewModel _mainWindowViewModel;
        
        public UserViewModel( IAuthService authService, IServiceProvider serviceProvider, IUserWorkPositionService userWorkPositionService,IUserService userService,MainWindowViewModel mainWindowViewModel,IWorkPositionService workPositionService )
        {
            _serviceProvider = serviceProvider;
            _userWorkPositionService = userWorkPositionService;
            _userService = userService;
            _workPositionService = workPositionService;
            _mainWindowViewModel = mainWindowViewModel;
            LogoutCommand = ReactiveCommand.CreateFromTask(LogoutAsync);
        }

        
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

        private string _workPositionDescription;
        public string WorkPositionDescription
        {
            get => _workPositionDescription;
            private set => this.RaiseAndSetIfChanged(ref _workPositionDescription, value);
        }

        public ReactiveCommand<Unit, Unit> LogoutCommand { get; }

        
        public async void SetUser(User user)
        {
            _user = user;
            var user1 = await _userService.GetUserByUsernameAsync(user.Username);
            var userWorkPosition = await _userWorkPositionService.FindUserWorkPositionByIdAsync(user1.Id);
            var workPosition = await _workPositionService.GetWorkPositionByIdAsync(userWorkPosition.WorkPositionId);


            FirstName = _user.FirstName;
            LastName = _user.LastName;
            RoleName = _user.Role?.Name ?? "No role assigned";

            if (userWorkPosition != null)
            {
                WorkPositionName = workPosition.Name ?? "N/A";
                WorkPositionProductName = userWorkPosition.ProductName ?? "N/A";
                AssignedDate = userWorkPosition.Date.ToString("dd.MM.yyyy") ?? "N/A";
                WorkPositionDescription = workPosition.Description ?? "N/A";
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
            _mainWindowViewModel.CurrentViewModel = loginViewModel;
        }
    }
}