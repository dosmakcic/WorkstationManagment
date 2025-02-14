using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using WorkstationManagment.Core.Models;
using WorkstationManagment.Core.Repositories;
using WorkstationManagment.ViewModels;

namespace WorkstationManagment.UI.ViewModels
{
    public class AdminViewModel :ViewModelBase
    {
        
        private readonly IUserRepository _userRepository;
        public ReactiveCommand<Unit, Unit> AddUserCommand { get; }

        private string _username;
        private string _firstName;
        private string _lastName;
        private string _password;
        private Role _role;

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => this.RaiseAndSetIfChanged(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => this.RaiseAndSetIfChanged(ref _lastName, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public Role Role
        {
            get => _role;
            set => this.RaiseAndSetIfChanged(ref _role, value);
        }

        public AdminViewModel (IUserRepository userRepository)
        {
            _userRepository = userRepository;
            AddUserCommand = ReactiveCommand.CreateFromTask(AddUserAsync);
        }


        public ReactiveCommand<Unit, Task> ChangeRoleCommand { get; }
        public ReactiveCommand<Unit, Task> CreateNewUserCommand { get; }



        private async Task AddUserAsync()
        {
            // Validacija unosa
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Role.Name)|| string.IsNullOrEmpty(Role.Description))
            {
                // Prikazivanje obavijesti o pogrešci (ako je potrebno)
                return;
            }

            var newUser = new User
            {
                Username = Username,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password, // Ovdje trebaš dodati enkripciju lozinke
                Role = Role
            };

            // Dodavanje korisnika putem servisa
            await _userRepository.AddUserAsync(newUser);
        }

    }
}
