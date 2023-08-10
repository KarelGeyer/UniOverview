using System;
using System.ComponentModel;
using System.Linq;
using UniOverview.Services;
using UniOverview.ViewModels;
using UniOverview.ViewModels.Subjects;

namespace UniOverview.Commands
{
	public class NavigationCommand<TViewModel> : CommandBase
		where TViewModel : ViewModelBase
	{
		private readonly NavigationService NavigationService;
		private readonly SubjectsBaseViewModel? SubjectsBaseViewModel;

		public NavigationCommand(NavigationService _navigationService, SubjectsBaseViewModel? _subjectsBaseViewModel = null)
		{
			NavigationService = _navigationService;
			SubjectsBaseViewModel = _subjectsBaseViewModel;
		}

		public override void Execute(object? parameter)
		{
			if (parameter is null)
				throw new ArgumentNullException(nameof(parameter));

			if (parameter is Guid)
			{
				if (SubjectsBaseViewModel.Equals(null))
					return;

				SubjectsBaseViewModel.ManageChildView((Guid)parameter);
				NavigationService.CurrentViewModel = NavigationService.GetExistingViewModels
					.Where(x => x.Key == "SubjectDetail")
					.First()
					.Value;

				return;
			}

			NavigationService.CurrentViewModel = NavigationService.GetExistingViewModels
				.Where(x => x.Key == parameter as string)
				.First()
				.Value;
		}

		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnCanExecuteChanged();
		}
	}
}
