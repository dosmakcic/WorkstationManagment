using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using WorkstationManagment.Core.Models;
using WorkstationManagment.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations;


namespace WorkstationManagment.UI.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly IUserWorkPositionService _userWorkPositionService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRoleService _roleService;
        private readonly IWorkPositionService _workPositionService;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private string _newFirstName = string.Empty;
        private string _newLastName = string.Empty;
        private string _newUsername = string.Empty;
        private string _newPassword = string.Empty;
        private string _newWorkPositionName = string.Empty;
        private string _newWorkPositionDescription = string.Empty;
        private int _newRoleId;
        private string _newProductName = string.Empty;
        private WorkPosition? _newUserWorkPosition;
        private Role? _newRole;
        private WorkPosition? _selectedWorkPosition;


        public AdminViewModel(IUserService userService, IUserWorkPositionService userWorkPositionService,IWorkPositionService workPositionService,IRoleService roleService, IServiceProvider serviceProvider,MainWindowViewModel mainWindowViewModel)
        {
            _userService = userService;
            _userWorkPositionService = userWorkPositionService;
            _workPositionService = workPositionService;
            _serviceProvider = serviceProvider;
            _roleService = roleService;
            _mainWindowViewModel = mainWindowViewModel;
            Users = new ObservableCollection<User>();
            WorkPositions = new ObservableCollection<WorkPosition>();
            UserWorkPositions = new ObservableCollection<UserWorkPosition>();
            Roles = new ObservableCollection<Role>();


            ChangeUserRoleCommand = ReactiveCommand.CreateFromTask(ChangeUserWorkPositionAsync);
            LogoutCommand = ReactiveCommand.CreateFromTask(LogoutAsync);
            AddNewUserCommand = ReactiveCommand.CreateFromTask(AddNewUserAsync);
            DeleteUserWorkPositionCommand = ReactiveCommand.CreateFromTask(DeleteUserWorkPositionAsync);
            AddNewWorkPositionCommand = ReactiveCommand.CreateFromTask(AddNewWorkPositionAsync);


            InitializeAsync();


        }
      
        public ObservableCollection<User> Users { get; }
        public ObservableCollection<WorkPosition> WorkPositions { get; }
        public ObservableCollection<UserWorkPosition> UserWorkPositions { get; } 

        public ObservableCollection<Role> Roles { get; }

   
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
        public string NewWorkPositionName { get => _newWorkPositionName; set => this.RaiseAndSetIfChanged(ref _newWorkPositionName, value); }
        public string NewWorkPositionDescription { get => _newWorkPositionDescription; set => this.RaiseAndSetIfChanged(ref _newWorkPositionDescription, value); }
        public int NewRoleId { get => _newRoleId; set => this.RaiseAndSetIfChanged(ref _newRoleId, value); }
        public WorkPosition? NewUserWorkPosition { get => _newUserWorkPosition; set => this.RaiseAndSetIfChanged(ref _newUserWorkPosition, value); }
        public string NewProductName { get => _newProductName; set => this.RaiseAndSetIfChanged(ref _newProductName, value); }
        public Role? NewRole { get => _newRole; set => this.RaiseAndSetIfChanged(ref _newRole, value); }


        public ReactiveCommand<Unit, Unit> ChangeUserRoleCommand { get; }
        public ReactiveCommand<Unit, Unit> LogoutCommand { get; }
        public ReactiveCommand<Unit, Unit> AddNewUserCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteUserWorkPositionCommand { get; }
        public ReactiveCommand<Unit, Unit> AddNewWorkPositionCommand { get; }




        private async Task LoadUserWorkPositionDataAsync()
        {
            var userWorkPositions = await _userWorkPositionService.GetAllUserWorkPositionsAsync();
            UserWorkPositions.Clear();

            foreach (var uwp in userWorkPositions)
            {
                UserWorkPositions.Add(uwp);
            }

            this.RaisePropertyChanged(nameof(UserWorkPositions));
        }

        private async Task LoadWorkPositionsDataAsync()
        {
            var positions = await _workPositionService.GetAllWorkPositionsAsync();
            WorkPositions.Clear();
            foreach (var position in positions)
            {
                WorkPositions.Add(position);
            }
            this.RaisePropertyChanged(nameof(WorkPositions));
        }


        private async Task LoadRolesDataAsync()
        {
            var roles = await _roleService.GetAllRoles();
            Roles.Clear();
            foreach (var role in roles)
            {
                Roles.Add(role);
            }
            this.RaisePropertyChanged(nameof(Roles));
        }


        private async Task ChangeUserWorkPositionAsync()
        {
            if (SelectedUserWorkPosition == null || SelectedWorkPosition == null)
            {
                return; 
            }

            
            await _userWorkPositionService.RemoveWorkPositionAsync(SelectedUserWorkPosition.UserId, SelectedUserWorkPosition.WorkPositionId);

            await _userWorkPositionService.AssignWorkPositionAsync(SelectedUserWorkPosition.UserId, SelectedWorkPosition.Id,NewProductName);

            await LoadUserWorkPositionDataAsync();
        }

       
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
                    RoleId = NewRole.Id,
                   
                };

                await _userService.AddUserAsync(newUser);
                var user1 = await _userService.GetUserByUsernameAsync(newUser.Username);
                await _userWorkPositionService.AssignWorkPositionAsync(user1.Id, NewUserWorkPosition.Id,NewProductName);
                await LoadUserWorkPositionDataAsync();

               
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

        private async Task DeleteUserWorkPositionAsync()
        {
            if (SelectedUserWorkPosition == null)
                return;

            try
            {
                var selectedUserWorkPosition = SelectedUserWorkPosition;
                
                await _userWorkPositionService.RemoveWorkPositionAsync(selectedUserWorkPosition.UserId, selectedUserWorkPosition.WorkPositionId);
                await _userService.DeleteUserAsync(selectedUserWorkPosition.UserId); 
                UserWorkPositions.Remove(selectedUserWorkPosition);
                await LoadUserWorkPositionDataAsync();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error: {ex.Message}");
            }
            return;

        }

        private async Task AddNewWorkPositionAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NewWorkPositionName) || string.IsNullOrWhiteSpace(NewWorkPositionDescription))
                {
                    Console.WriteLine("Missing data");
                    return;
                }
                var newWorkPosition = new WorkPosition { Name = NewWorkPositionName, Description = NewWorkPositionDescription };
                await _workPositionService.AddWorkPositionAsync(newWorkPosition);
                await LoadWorkPositionsDataAsync();

                NewWorkPositionName = string.Empty;
                NewWorkPositionDescription = string.Empty;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while adding user: {ex.Message}");
            }
        }


        private async Task LogoutAsync()
        {
            await Task.Delay(100);
            var loginViewModel = _serviceProvider.GetRequiredService<LoginViewModel>();
            _mainWindowViewModel.CurrentViewModel = loginViewModel;
        }

        private async Task InitializeAsync()
        {
            try
            {
                await LoadUserWorkPositionDataAsync();
                await LoadWorkPositionsDataAsync();
                await LoadRolesDataAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }
    }

}
