using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniOverview.Commands;
using UniOverview.Enums.Subjects;
using UniOverview.Helpers;
using UniOverview.Models;
using UniOverview.Models.Subjects;
using UniOverview.Models.Teachers;
using UniOverview.Services;
using UniOverview.Services.mock;

namespace UniOverview.ViewModels.Subjects
{
	public class AddNewSubjectViewModel : ViewModelBase
	{
		#region Private Properties
		private SubjectCollectionDataService SubjectCollectionDataService { get; set; }
		private TeachersCollectionDataService TeachersCollectionDataService { get; set; }
		#endregion

		#region Public Properties
		public string? SubjectName { get; set; }
		public string? ShortName { get; set; }
		public string? TeacherName { get; set; }
		public string? TeacherSurname { get; set; }
		public string? TeacherEmail { get; set; }
		public string? TeachersDegree { get; set; }
		public string? TypeOfSubject { get; set; }
		public string? SubjectTypeOfCompletion { get; set; }
		public string? SubjectFormOfCompletion { get; set; }
		public int Credits { get; set; }
		public ObservableCollection<TeachersName> TeachersNames { get; set; }
		public bool IsChoosingTeacher { get; set; } = true;
		public TeachersName ChosenTeacherName { get; set; }
		public string[] Titles { get; set; }
		#endregion

		public ICommand AddSubjectCommand { get; set; }
		public ICommand SetIsChoosingTeacherCommand { get; set; }
		public ICommand NavigationCommand { get; set; }

		public AddNewSubjectViewModel(
			SubjectCollectionDataService subjectCollectionDataService,
			NavigationService navigationService,
			TeachersCollectionDataService teachersCollectionDataService
		)
		{
			AddSubjectCommand = new BaseButtonCommand(null, AddNewSubject);
			SetIsChoosingTeacherCommand = new BaseButtonCommand(null, ChangeIsChoosingTeacher);
			NavigationCommand = new NavigationCommand<ViewModelBase>(navigationService);

			TeachersCollectionDataService = teachersCollectionDataService;
			SubjectCollectionDataService = subjectCollectionDataService;

			SetTeachersNames(TeachersCollectionDataService.GetTeachersFullNames());
			SetTitles(TeachersCollectionDataService.GetAllTitles());
		}

		private void AddNewSubject()
		{
			string[] stringsToValidate = { SubjectName, ShortName };
			bool areBasicStringValid = FormValidation.ValidateBasicString(stringsToValidate);
			if (!areBasicStringValid)
				return;

			int[] integersToValidate = { Credits };
			bool areAllIntegersHigherThenZero = FormValidation.ValidateIntegersAndFloatsHigherThenZero(integersToValidate, null);
			if (!areAllIntegersHigherThenZero)
				return;

			CompletionDetails completionDetails = FormValidation.ValidateAddNewSubjectCompletionDetails(
				TypeOfSubject,
				SubjectTypeOfCompletion,
				SubjectFormOfCompletion
			);

			Teacher? teacher;
			if (IsChoosingTeacher)
			{
				if (string.IsNullOrEmpty(ChosenTeacherName.Name))
					return;
				bool isTeacherNameValid = FormValidation.ValidateBasicString(ChosenTeacherName.Name);
				if (!isTeacherNameValid)
					return;

				teacher = TeachersCollectionDataService.GetTeacherByName(ChosenTeacherName.Name);
				if (teacher == null)
					return;
			}
			else
			{
				string[] stringsTovalidate = { TeacherName, TeacherSurname, TeachersDegree };
				bool areTeachersDataValid = FormValidation.ValidateBasicString(stringsTovalidate);
				string email = FormValidation.ValidateEmail(TeacherEmail);

				if (!areTeachersDataValid)
					return;

				teacher = new()
				{
					Id = Guid.NewGuid(),
					Name = TeacherName!,
					Surname = TeacherSurname!,
					Email = email,
					Title = TeachersDegree,
				};

				TeachersCollectionDataService.AddTeacher(teacher);
			}

			Subject newSubject =
				new()
				{
					Id = new Guid(),
					Name = SubjectName,
					ShortName = ShortName,
					IsCompleted = false,
					IsFailed = false,
					Credits = Credits,
					Points = 0,
					Teacher = teacher,
					ExamHistory = new List<Exam>(),
					Homeworks = new List<Homework>(),
					Grade = Enums.Grade.NO_GRADE,
					SubjectType = completionDetails.TypeOfSubject,
					SubjectTypeIcon = SubjectCollectionDataService.GetSubjectTypeIcon(completionDetails.TypeOfSubject),
					SubjectTypeColor = "#FF0000",
					TypeOfCompletion = completionDetails.SubjectTypeOfCompletion,
					FormOfCompletion = completionDetails.SubjectFormOfCompletion,
				};

			SubjectCollectionDataService.AddSubject(newSubject);
			ResetForm();

			NavigationCommand.Execute("Subjects");
		}

		private void ResetForm()
		{
			SubjectName = "";
			ShortName = "";
			Credits = 0;
			TeacherName = "";
			TeacherSurname = "";
			TeacherEmail = "";
			TeachersDegree = "";
			TypeOfSubject = "";
			SubjectTypeOfCompletion = "";
			SubjectFormOfCompletion = "";
		}

		private void ChangeIsChoosingTeacher()
		{
			SetIsChoosingTeacher(!IsChoosingTeacher);
		}

		#region SETTERS
		private void SetTeachersNames(ObservableCollection<TeachersName> values)
		{
			TeachersNames = values;
			OnPropertyChanged(nameof(TeachersNames));
		}

		private void SetTitles(string[] values)
		{
			Titles = values;
			OnPropertyChanged(nameof(Titles));
		}

		private void SetIsChoosingTeacher(bool value)
		{
			IsChoosingTeacher = value;
			OnPropertyChanged(nameof(IsChoosingTeacher));
		}
		#endregion
	}
}
