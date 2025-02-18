using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkstationManagment.Core.Services
{
   public class NavigationService : INavigationService
    {
        private readonly IScreen _screen;

        public NavigationService(IScreen screen)
        {
            _screen = screen;
        }

        public void NavigateTo(IRoutableViewModel viewModel)
        {
            _screen.Router.Navigate.Execute(viewModel);
        }
    }
}
