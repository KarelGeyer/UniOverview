using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniOverview.Services;
using UniOverview.ViewModels;
using UniOverview.ViewModels.Home;
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
				throw new ArgumentNullException(nameof(parameter));

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
