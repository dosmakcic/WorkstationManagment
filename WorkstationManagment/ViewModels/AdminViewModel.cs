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
using Org.BouncyCastle.Asn1.X509;


namespace WorkstationManagment.UI.ViewModels
{
    public class AdminViewModel : ViewModelBase, IRoutableViewModel
    {
        private readonly IUserService _userService;
        private readonly IWorkPositionService _workPositionService;
        private readonly INavigationService _navigationService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRoleService _roleService;


        private string _newFirstName = string.Empty;
        private string _newLastName = string.Empty;
        private string _newUsername = string.Empty;
        private string _newPassword = string.Empty;
        private int _newRoleId;
        private string _newProductName = string.Empty;
        private WorkPosition? _newUserWorkPosition;
        private Role? _newRole;


        private User? _selectedUser;
        private WorkPosition? _selectedWorkPosition;


        public AdminViewModel(IScreen screen,IUserService userService, IWorkPositionService workPositionService,IRoleService roleService, INavigationService navigationService, IServiceProvider serviceProvider)
        {
            HostScreen = screen ?? throw new ArgumentNullException(nameof(screen));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _workPositionService = workPositionService ?? throw new ArgumentNullException(nameof(workPositionService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _serviceProvider = serviceProvider;
            _roleService = roleService;

            Users = new ObservableCollection<User>();
            WorkPositions = new ObservableCollection<WorkPosition>();
            UserWorkPositions = new ObservableCollection<UserWorkPosition>();
            Roles = new ObservableCollection<Role>();


            ChangeUserRoleCommand = ReactiveCommand.CreateFromTask(ChangeUserWorkPositionAsync);
            //RemoveWorkPositionCommand = ReactiveCommand.CreateFromTask(RemoveWorkPositionAsync);
            LoadDataCommand = ReactiveCommand.CreateFromTask(LoadDataAsync);
            LogoutCommand = ReactiveCommand.CreateFromTask(LogoutAsync);
            AddNewUserCommand = ReactiveCommand.CreateFromTask(AddNewUserAsync);

           
            LoadDataCommand.Execute().Subscribe();
        }

        public string? UrlPathSegment => "admin";
        public IScreen HostScreen { get; }

        public ObservableCollection<User> Users { get; }
        public ObservableCollection<WorkPosition> WorkPositions { get; }
        public ObservableCollection<UserWorkPosition> UserWorkPositions { get; } 

        public ObservableCollection<Role> Roles { get; }

        //public User? SelectedUser
        //{
        //    get => _selectedUser;
        //    set => this.RaiseAndSetIfChanged(ref _selectedUser, value);
        //}


        private UserWorkPosition? _selectedUserWorkPosition;

        public UserWorkPosition? SelectedUserWorkPosition
        {
            get => _selectedUserWorkPosition;
            set => this.RaiseAndSetIfChanged(ref _selectedUserWorkPosition, value);
        }

        public WorkPosition? SelectedWorkPosition
        {
            get => _selectedWorkPosition;
            set => this.RaiseAndSetIfChanged(ref _selectedWorkPosition, value);
        }



        public string NewFirstName { get => _newFirstName; set => this.RaiseAndSetIfChanged(ref _newFirstName, value); }
        public string NewLastName { get => _newLastName; set => this.RaiseAndSetIfChanged(ref _newLastName, value); }
        public string NewUsername { get => _newUsername; set => this.RaiseAndSetIfChanged(ref _newUsername, value); }
        public string NewPassword { get => _newPassword; set => this.RaiseAndSetIfChanged(ref _newPassword, value); }
        public int NewRoleId { get => _newRoleId; set => this.RaiseAndSetIfChanged(ref _newRoleId, value); }
        public WorkPosition? NewUserWorkPosition { get => _newUserWorkPosition; set => this.RaiseAndSetIfChanged(ref _newUserWorkPosition, value); }
        public string NewProductName { get => _newProductName; set => this.RaiseAndSetIfChanged(ref _newProductName, value); }
        public Role? NewRole { get => _newRole; set => this.RaiseAndSetIfChanged(ref _newRole, value); }
        public ReactiveCommand<Unit, Unit> ChangeUserRoleCommand { get; }

        public ReactiveCommand<Unit, Unit> RemoveWorkPositionCommand { get; }
        public ReactiveCommand<Unit, Unit> LoadDataCommand { get; }
        public ReactiveCommand<Unit, Unit> LogoutCommand { get; }

        public ReactiveCommand<Unit, Unit> AddNewUserCommand { get; }
       


        private async Task LoadDataAsync()
        {
            var userWorkPositions = await _workPositionService.GetAllUserWorkPositionsAsync();
            UserWorkPositions.Clear();

            foreach (var uwp in userWorkPositions)
            {
                UserWorkPositions.Add(uwp);
            }

            this.RaisePropertyChanged(nameof(UserWorkPositions));

            var positions = await _workPositionService.GetAllWorkPositionsAsync();
            WorkPositions.Clear();
            foreach (var position in positions)
            {
                WorkPositions.Add(position);
            }

            var roles = await _roleService.GetAllRoles();
            Roles.Clear();
            foreach(var role in roles)
            {
                Roles.Add(role);
            }
        }

        private async Task ChangeUserWorkPositionAsync()
        {
            if (SelectedUserWorkPosition == null || SelectedWorkPosition == null)
            {
                return; 
            }

            
            await _workPositionService.RemoveWorkPositionAsync(SelectedUserWorkPosition.UserId, SelectedUserWorkPosition.WorkPositionId);

            
            await _workPositionService.AssignWorkPositionAsync(SelectedUserWorkPosition.UserId, SelectedWorkPosition.Id,NewProductName);

           
            await LoadDataAsync();
        }

        //private async Task RemoveWorkPositionAsync()
        //{
        //    if (SelectedUser == null || SelectedWorkPosition == null) return;

        //    await _workPositionService.RemoveWorkPositionAsync(SelectedUser.Id, SelectedWorkPosition.Id);
        //    await LoadDataAsync();
        //}

        private async Task AddNewUserAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NewUsername) || NewUserWorkPosition == null || NewRole == null || string.IsNullOrWhiteSpace(NewPassword)|| string.IsNullOrWhiteSpace(NewFirstName)|| string.IsNullOrWhiteSpace(NewLastName))
                {
                    Console.WriteLine("Missing data");
                    return;
                }

                var existingUser = await _userService.GetUserByUsernameAsync(NewUsername);
                if (existingUser != null)
                {
                    Console.WriteLine("User already exist");
                    return;
                }

                var newUser = new User
                {
                    FirstName = NewFirstName,
                    LastName = NewLastName,
                    Username = NewUsername,
                    Password = NewPassword,
                    RoleId = NewRole.Id 
                };

                await _userService.AddUserAsync(newUser);
                await _workPositionService.AssignWorkPositionAsync(newUser.Id, NewUserWorkPosition.Id,NewProductName);
                await LoadDataAsync();

               
                NewFirstName = string.Empty;
                NewLastName = string.Empty;
                NewUsername = string.Empty;
                NewPassword = string.Empty;
                NewProductName = string.Empty;
                NewRole = null;
                NewUserWorkPosition = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while adding user: {ex.Message}");
            }
        }




        private async Task LogoutAsync()
        {
            await Task.Delay(100);
            var loginViewModel = App.ServiceProvider.GetRequiredService<LoginViewModel>();
            _navigationService.NavigateTo(loginViewModel);
        }
    }
}
