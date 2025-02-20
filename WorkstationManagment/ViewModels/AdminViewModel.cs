using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using WorkstationManagment.Core.Models;
using WorkstationManagment.Core.Services;
using WorkstationManagment.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Splat;


namespace WorkstationManagment.UI.ViewModels
{
    public class AdminViewModel : ViewModelBase, IRoutableViewModel
    {
        private readonly IUserService _userService;
        private readonly IWorkPositionService _workPositionService;
        private readonly INavigationService _navigationService;
        

        private User? _selectedUser;
        private WorkPosition? _selectedWorkPosition;

        public AdminViewModel(IScreen screen, User user,IUserService userService, IWorkPositionService workPositionService, INavigationService navigationService)
        {
            HostScreen = screen ?? throw new ArgumentNullException(nameof(screen));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _workPositionService = workPositionService ?? throw new ArgumentNullException(nameof(workPositionService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            

            Users = new ObservableCollection<User>();
            WorkPositions = new ObservableCollection<WorkPosition>();

            ChangeUserRoleCommand = ReactiveCommand.CreateFromTask(ChangeUserRoleAsync);
            RemoveWorkPositionCommand = ReactiveCommand.CreateFromTask(RemoveWorkPositionAsync);
            LoadDataCommand = ReactiveCommand.CreateFromTask(LoadDataAsync);
            LogoutCommand = ReactiveCommand.CreateFromTask(LogoutAsync);

            LoadDataCommand.Execute().Subscribe();
        }

        public string? UrlPathSegment => "admin";
        public IScreen HostScreen { get; }

        public ObservableCollection<User> Users { get; }
        public ObservableCollection<WorkPosition> WorkPositions { get; }

        public User? SelectedUser
        {
            get => _selectedUser;
            set => this.RaiseAndSetIfChanged(ref _selectedUser, value);
        }

        public WorkPosition? SelectedWorkPosition
        {
            get => _selectedWorkPosition;
            set => this.RaiseAndSetIfChanged(ref _selectedWorkPosition, value);
        }

        public ReactiveCommand<Unit, Unit> ChangeUserRoleCommand { get; }

        public ReactiveCommand<Unit, Unit> RemoveWorkPositionCommand { get; }
        public ReactiveCommand<Unit, Unit> LoadDataCommand { get; }
        public ReactiveCommand<Unit, Unit> LogoutCommand { get; }

        private async Task LoadDataAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }

            var workPositions = await _workPositionService.GetAllWorkPositionsAsync();
            WorkPositions.Clear();
            foreach (var workPosition in workPositions)
            {
                WorkPositions.Add(workPosition);
            }
        }

        private async Task ChangeUserRoleAsync()
        {
            if (SelectedUser == null || SelectedWorkPosition == null) return;

            await _workPositionService.AssignWorkPositionAsync(SelectedUser.Id, SelectedWorkPosition.Id);

            await LoadDataAsync();
        }

        private async Task RemoveWorkPositionAsync()
        {
            if (SelectedUser == null || SelectedWorkPosition == null) return;

            await _workPositionService.RemoveWorkPositionAsync(SelectedUser.Id, SelectedWorkPosition.Id);
            await LoadDataAsync();
        }

        private async Task LogoutAsync()
        {
            await Task.Delay(100);
            var loginViewModel = App.ServiceProvider.GetRequiredService<LoginViewModel>();
            _navigationService.NavigateTo(loginViewModel);
        }
    }
}
