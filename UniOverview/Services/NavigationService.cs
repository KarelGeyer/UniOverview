using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniOverview.ViewModels;

namespace UniOverview.Services
{
    public class NavigationService
    {
        public event Action CurrentViewModelChanged;
        private ViewModelBase ViewModel;
        private ViewModelBase HomeViewModel;
        private ViewModelBase MaterialsViewModel;
        private ViewModelBase SubjectsViewModel;

        private Dictionary<string, ViewModelBase> ViewModels;

        public NavigationService()
        {
            ViewModels = new Dictionary<string, ViewModelBase>();
        }

        public ViewModelBase CurrentViewModel
        {
            get => ViewModel;
            set
            {
                ViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public ViewModelBase NavHomeViewModel
        {
            get => HomeViewModel;
            set
            {
                HomeViewModel = value;
                ViewModels.Add("Home", value);
            }
        }
        public ViewModelBase NavMaterialsViewModel
        {
            get => MaterialsViewModel;
            set
            {
                MaterialsViewModel = value;
                ViewModels.Add("Materials", value);
            }
        }
        public ViewModelBase NavSubjectsViewModel
        {
            get => SubjectsViewModel;
            set
            {
                SubjectsViewModel = value;
                ViewModels.Add("Subjects", value);
            }
        }

        public Dictionary<string, ViewModelBase> GetExistingViewModels
        {
            get => ViewModels;
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
