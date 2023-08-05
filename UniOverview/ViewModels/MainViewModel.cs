using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniOverview.Commands;
using UniOverview.Services;
using UniOverview.ViewModels.Grades;

namespace UniOverview.ViewModels
{
    public class SideMenuViewModel : ViewModelBase
    {
        private readonly NavigationService NavigationService;
        public ViewModelBase CurrentViewModel => NavigationService.CurrentViewModel;
        public ICommand NavigationCommand { get; set; }

        public SideMenuViewModel(NavigationService navigationService) { }
    }
}
