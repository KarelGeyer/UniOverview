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
using UniOverview.Services;

namespace UniOverview.ViewModels.Subjects
{
	public class SubjectsBaseViewModel : ViewModelBase
	{
		private readonly ObservableCollection<Subject> _subjectsCollection = new();
		private readonly NavigationService NavigationService;
		private readonly SubjectViewModel SubjectViewModel;

		public ViewModelBase CurrentViewModel => NavigationService.CurrentViewModel;
		public IEnumerable<Subject> Subjects => _subjectsCollection;
		public ICommand NavigationCommand { get; set; }

		public SubjectsBaseViewModel(SubjectViewModel _subjectViewModel, NavigationService navigationService)
		{
			NavigationService = navigationService;
			SubjectViewModel = _subjectViewModel;

			Init();

			Teacher teacherOne = new Teacher()
			{
				Id = Guid.NewGuid(),
				Name = "John",
				Surname = "Doe",
				Email = "JohnDoe@test.com"
			};

			Subject subjectOne = new Subject()
			{
				Id = Guid.NewGuid(),
				Name = "Architektura Počítačů a kryptografie",
				ShortName = "Math",
				IsCompleted = false,
				IsFailed = false,
				Credits = 5,
				Points = 0,
				Teacher = teacherOne,
				ExamHistory = new List<Exam>(),
				Homeworks = new List<Homework>(),
				Grade = Grade.NO_GRADE,
				SubjectType = Enums.Subjects.SubjectType.A,
				SubjectTypeColor = "#FF0000",
				SubjectTypeIcon = GetSubjectTypeIcon(Enums.Subjects.SubjectType.A),
				TypeOfCompletion = Enums.Subjects.TypeOfCompletion.Combined,
				FormOfCompletion = Enums.Subjects.FormOfCompletion.Exam,
			};

			Subject subjectTwo = new Subject()
			{
				Id = Guid.NewGuid(),
				Name = "Základy Informatiky",
				ShortName = "CS",
				IsCompleted = false,
				IsFailed = false,
				Credits = 5,
				Points = 0,
				Teacher = teacherOne,
				ExamHistory = new List<Exam>(),
				Homeworks = new List<Homework>(),
				Grade = Grade.NO_GRADE,
				SubjectType = Enums.Subjects.SubjectType.B,
				SubjectTypeColor = "#FF0000",
				SubjectTypeIcon = GetSubjectTypeIcon(Enums.Subjects.SubjectType.B),
				TypeOfCompletion = Enums.Subjects.TypeOfCompletion.Combined,
				FormOfCompletion = Enums.Subjects.FormOfCompletion.Exam,
			};

			Exam exam = new Exam()
			{
				Id = Guid.NewGuid(),
				SubjectId = subjectOne.Id,
				Points = 0,
				Date = DateTime.Now,
				Result = ExamResult.Failed,
				Participated = true,
				Grade = Grade.F
			};

			Exam exam2 = new Exam()
			{
				Id = Guid.NewGuid(),
				SubjectId = subjectOne.Id,
				Points = 75,
				Date = DateTime.Now,
				Result = ExamResult.Passed,
				Participated = true,
				Grade = Grade.C
			};

			Homework homework = new Homework()
			{
				Id = Guid.NewGuid(),
				SubjectId = subjectOne.Id,
				Name = "Homework 1",
				Type = HomeworkType.SemesterProject,
				DateToComlete = DateTime.Now,
				DateOfCompletion = DateTime.Now,
				IsCompleted = false,
				MaxPoints = 10,
				Points = 0
			};

			Homework homework2 = new Homework()
			{
				Id = Guid.NewGuid(),
				SubjectId = subjectOne.Id,
				Name = "Homework 1",
				Type = HomeworkType.Essay,
				DateToComlete = DateTime.Now,
				DateOfCompletion = DateTime.Now,
				IsCompleted = true,
				MaxPoints = 10,
				Points = 10
			};

			subjectOne.ExamHistory.Add(exam);
			subjectOne.ExamHistory.Add(exam2);

			subjectOne.Homeworks.Add(homework);
			subjectOne.Homeworks.Add(homework2);

			subjectTwo.ExamHistory.Add(exam);
			subjectTwo.ExamHistory.Add(exam2);

			subjectTwo.Homeworks.Add(homework);
			subjectTwo.Homeworks.Add(homework2);

			_subjectsCollection.Add(subjectOne);
			_subjectsCollection.Add(subjectTwo);
		}

		private void Init()
		{
			NavigationService.CurrentViewModelChanged += OnChangeView;
			NavigationCommand = new NavigationCommand<ViewModelBase>(NavigationService, this);
		}

		public string GetSubjectTypeIcon(SubjectType subjectType)
		{
			if (subjectType == SubjectType.A)
				return nameof(SubjectTypeIcon.AlphaACircleOutline);
			if (subjectType == SubjectType.B)
				return nameof(SubjectTypeIcon.AlphaBCircleOutline);

			return nameof(SubjectTypeIcon.AlphaCCircleOutline);
		}

		public Subject GetSelectedSubject(Guid id)
		{
			Subject? selectedSubject = _subjectsCollection.Where(subject => subject.Id.Equals(id)).FirstOrDefault();

			if (selectedSubject == null)
			{
				throw new Exception("Subject not found");
			}

			return selectedSubject;
		}

		public void ManageChildView(Guid id)
		{
			SubjectViewModel.SetCurrentSubject(GetSelectedSubject(id));
		}

		public void OnChangeView()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}
	}
}
