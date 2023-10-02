using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UniOverview.Commands;
using UniOverview.Enums;
using UniOverview.Enums.Subjects;
using UniOverview.Models;
using UniOverview.Models.Subjects;
using UniOverview.Models.Teachers;
using UniOverview.Services;
using UniOverview.Services.mock;

namespace UniOverview.ViewModels.Teachers
{
	public class TeachersBaseViewModel : ViewModelBase
	{
		private readonly NavigationService NavigationService;
		private readonly TeachersCollectionDataService TeachersCollectionDataService;

		public ViewModelBase CurrentViewModel => NavigationService.CurrentViewModel;
		public IEnumerable<Teacher> Teachers { get; set; }
		public ICommand NavigationCommand { get; set; }
		public ICommand ShowTeachersSubjectsCommand { get; set; }

		public TeachersBaseViewModel(NavigationService navigationService, TeachersCollectionDataService teachersCollectionDataService)
		{
			NavigationService = navigationService;
			TeachersCollectionDataService = teachersCollectionDataService;
			Teachers = teachersCollectionDataService.GetTeachers();

			NavigationCommand = new NavigationCommand<ViewModelBase>(NavigationService);
			ShowTeachersSubjectsCommand = new BaseButtonCommand(RedirectToFilteredSubjectsPage);
		}

		public void RedirectToFilteredSubjectsPage(object? id)
		{
			string? convertedId = id?.ToString();
			if (convertedId == null)
				return;

			Guid teacherId = Guid.Parse(convertedId);
			TeachersCollectionDataService.SetTeacher(teacherId);
			NavigationCommand.Execute("Subjects");
		}
	}
}
