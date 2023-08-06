using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniOverview.Models.Subjects;
using UniOverview.Services;
using UniOverview.ViewModels.Subjects;

namespace UniOverview.Commands.Subjects
{
	public class RedirectToSubjectDetailCommand : CommandBase
	{
		private readonly CommandBase NavigationCommand;
		private readonly SubjectsBaseViewModel SubjectsBaseViewModel;
		private readonly SubjectViewModel SubjectViewModel;

		public RedirectToSubjectDetailCommand(
			CommandBase _navigationCommand,
			SubjectsBaseViewModel _subjectsBaseViewModel,
			SubjectViewModel _subjectViewModel
		)
		{
			NavigationCommand = _navigationCommand;
			SubjectsBaseViewModel = _subjectsBaseViewModel;
			SubjectViewModel = _subjectViewModel;
		}

		public override void Execute(object? parameter)
		{
			if (parameter is null || parameter is not string)
				throw new ArgumentNullException(nameof(parameter));

			Guid subjectId = Guid.Parse(parameter as string);
			Subject currentSubject = SubjectsBaseViewModel.GetSelectedSubject(subjectId);
			SubjectViewModel.CurrentSubject = currentSubject;

			NavigationCommand.Execute(parameter);
		}

		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnCanExecuteChanged();
		}
	}
}
