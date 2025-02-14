using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using WorkstationManagment.Core.Services;
using WorkstationManagment.ViewModels;

namespace WorkstationManagment.UI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;

        private string _username;
        private string _password;
        private bool _isAuthenticated;

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set => this.RaiseAndSetIfChanged(ref _isAuthenticated, value);
        }

        public ReactiveCommand<Unit, Unit> LoginCommand { get; }

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;

            // Kreiramo komandu za login
            LoginCommand = ReactiveCommand.CreateFromTask(LoginAsync);  
        }

        private async Task LoginAsync()
        {
            
            IsAuthenticated = await _authService.AuthenticateAsync(Username, Password);
            //ispraviti da vrati korisnika i ode na pocetnu od korisnika
        }

    }
}
