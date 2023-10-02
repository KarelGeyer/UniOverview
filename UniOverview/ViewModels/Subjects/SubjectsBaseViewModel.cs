using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using UniOverview.Commands;
using UniOverview.Enums;
using UniOverview.Enums.Subjects;
using UniOverview.Models;
using UniOverview.Models.Subjects;
using UniOverview.Models.Teachers;
using UniOverview.Services;
using UniOverview.Services.mock;

namespace UniOverview.ViewModels.Subjects
{
	public class SubjectsBaseViewModel : ViewModelBase
	{
		#region PRIVATE PROPERTIES
		private readonly NavigationService NavigationService;
		private readonly SubjectViewModel SubjectViewModel;
		private readonly SubjectCollectionDataService SubjectCollectionDataService;
		private readonly TeachersCollectionDataService TeachersCollectionDataService;
		#endregion

		#region FILTER
		private string _filter = "Alphabet";
		private SubjectsFilter Filter { get; set; } = SubjectsFilter.Alphabet;
		public string FilterOrder { get; set; } = "Ascending";
		public string FilterSetter
		{
			get { return _filter; }
			set
			{
				string newValue = value.Replace("System.Windows.Controls.ComboBoxItem: ", "");
				_filter = newValue;
				OnPropertyChanged(nameof(FilterSetter));
				AssignFilter(newValue);
			}
		}
		#endregion

		#region PUBLIC PROPERTIES
		public ViewModelBase CurrentViewModel => NavigationService.CurrentViewModel;
		public IEnumerable<Subject> Subjects { get; set; }
		public bool ShouldDisplayTeachersView { get; set; } = false;
		#endregion

		#region COMMANDS
		public ICommand NavigationCommand { get; set; }
		public ICommand ChangeFiltersOrderCommand { get; set; }
		public ICommand SetToInitialViewCommand { get; set; }
		#endregion



		public SubjectsBaseViewModel(
			SubjectViewModel _subjectViewModel,
			NavigationService navigationService,
			SubjectCollectionDataService subjectCollectionDataService,
			TeachersCollectionDataService teachersCollectionDataService
		)
		{
			NavigationService = navigationService;
			SubjectViewModel = _subjectViewModel;
			SubjectCollectionDataService = subjectCollectionDataService;
			TeachersCollectionDataService = teachersCollectionDataService;

			NavigationCommand = new NavigationCommand<ViewModelBase>(NavigationService, this);
			ChangeFiltersOrderCommand = new BaseButtonCommand(null, ChangeFilterOrder);
			SetToInitialViewCommand = new BaseButtonCommand(null, SetToInitialView);

			SetSubjects(subjectCollectionDataService.GetSubjects());
			NavigationService.CurrentViewModelChanged += OnChangeView;
		}

		public override void OnLoad()
		{
			FilterSubjects();
		}

		public void ManageChildView(Guid id)
		{
			SubjectViewModel.OnPageDisplay(SubjectCollectionDataService.GetSelectedSubject(id));
		}

		public void OnChangeView()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}

		public void AssignFilter(string value)
		{
			SubjectsFilter filter = (SubjectsFilter)Enum.Parse(typeof(SubjectsFilter), value);
			if (filter.Equals(null))
				return;
			Filter = filter;
			FilterSubjects();
		}

		public void ChangeFilterOrder()
		{
			if (FilterOrder == "Ascending")
				SetFilterOrder("Descending");
			else
				SetFilterOrder("Ascending");
			FilterSubjects();
		}

		public void FilterSubjects()
		{
			Teacher? currentTeacher = TeachersCollectionDataService.GetTeacher();

			if (currentTeacher != null)
			{
				SetSubjects(SubjectCollectionDataService.GetSubjects().Where(x => x.Teacher?.Id == currentTeacher.Id));
				ShouldDisplayTeachersView = true;
			}
			else
			{
				Func<Subject, object> keySelector;

				switch (Filter)
				{
					case SubjectsFilter.Alphabet:
						keySelector = x => x.Name;
						break;
					case SubjectsFilter.Completed:
						keySelector = x => x.IsCompleted;
						break;
					case SubjectsFilter.Failed:
						keySelector = x => x.IsFailed;
						break;
					case SubjectsFilter.SubjectType:
						keySelector = x => x.SubjectType!;
						break;
					default:
						keySelector = x => x.Name;
						break;
				}

				SetSubjects(
					FilterOrder.Equals("Ascending")
						? SubjectCollectionDataService.GetSubjects().OrderBy(keySelector)
						: SubjectCollectionDataService.GetSubjects().OrderByDescending(keySelector)
				);
			}
		}

		public void SetToInitialView()
		{
			SetShouldDisplayTeachersView(false);
			TeachersCollectionDataService.SetTeacher(null);
			AssignFilter("Alphabet");
		}

		#region SETTERS
		public void SetShouldDisplayTeachersView(bool value)
		{
			ShouldDisplayTeachersView = value;
			OnPropertyChanged(nameof(ShouldDisplayTeachersView));
		}

		public void SetFilterOrder(string value)
		{
			FilterOrder = value;
			OnPropertyChanged(nameof(FilterOrder));
		}

		public void SetSubjects(IEnumerable<Subject> subjects)
		{
			Subjects = subjects;
			OnPropertyChanged(nameof(Subjects));
		}
		#endregion
	}
}
