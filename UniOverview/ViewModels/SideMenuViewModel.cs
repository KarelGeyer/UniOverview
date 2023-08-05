using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniOverview.Commands;
using UniOverview.Services;
using UniOverview.ViewModels.Grades;
using UniOverview.ViewModels.Materials;

namespace UniOverview.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationService NavigationService;
        public ViewModelBase CurrentViewModel => NavigationService.CurrentViewModel;
        public ICommand NavigationCommand { get; set; }

        public MainViewModel(NavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigationService.CurrentViewModelChanged += OnChangeView;

            NavigationCommand = new NavigationCommand<ViewModelBase>(navigationService);
        }

        public void OnChangeView()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
