using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkstationManagment.ViewModels
{
    class WorkPositionViewModel : ViewModelBase
    {
        private string _positionName;
        private string _positionDescription;

        public string PositionName
        {
            get => _positionName;
            set => this.RaiseAndSetIfChanged(ref _positionName, value);
        }

        public string PositionDescription
        {
            get => _positionDescription;
            set => this.RaiseAndSetIfChanged(ref _positionDescription, value);
        }
    }
}
