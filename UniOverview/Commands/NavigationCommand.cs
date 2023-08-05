using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniOverview.Services;
using UniOverview.ViewModels;
using UniOverview.ViewModels.Grades;
using UniOverview.ViewModels.Materials;
using UniOverview.ViewModels.Subjects;

namespace UniOverview.Commands
{
    public class NavigationCommand<TViewModel> : CommandBase
        where TViewModel : ViewModelBase
    {
        private readonly NavigationService NavigationService;

        public NavigationCommand(NavigationService _navigationService)
        {
            NavigationService = _navigationService;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (parameter.Equals("Materials"))
            {
                NavigationService.CurrentViewModel = new MaterialsBaseViewModel();
            }

            if (parameter.Equals("Grades"))
            {
                NavigationService.CurrentViewModel = new GradesBaseViewModel();
            }

            if (parameter.Equals("Subjects"))
            {
                NavigationService.CurrentViewModel = new SubjectsBaseViewModel();
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }
    }
}
