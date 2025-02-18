using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkstationManagment.Core.Services
{
    public interface INavigationService
    {
        void NavigateTo(IRoutableViewModel viewModel);
    }
}
