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
        public ViewModelBase CurrentViewModel
        {
            get => ViewModel;
            set
            {
                ViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
