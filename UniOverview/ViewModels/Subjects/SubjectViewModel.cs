using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniOverview.Commands;
using UniOverview.Enums.Subjects;
using UniOverview.Models.Subjects;
using UniOverview.Services.mock;

namespace UniOverview.ViewModels.Subjects
{
	public class SubjectViewModel : ViewModelBase
	{
		#region Private Properties
		private Guid CurrentHomeworkId { get; set; }
		private SubjectCollectionDataService SubjectCollectionDataService { get; set; }
		#endregion

		#region Public Properties
		public Subject? CurrentSubject { get; set; }
		public SubjectDetailDisplayType ContentToBeDisplayed { get; set; }
		public HomeworkForm HomeworkForm { get; set; }
		public ExamForm ExamForm { get; set; }
		public int VisibileContent { get; set; } = 0;
		#endregion

		#region Commands
		public ICommand ButtonHandlerCommand { get; set; }
		public ICommand AddHomeWorkCommand { get; set; }
		public ICommand AddExamWorkCommand { get; set; }
		public ICommand MarkSubjectFailedCommand { get; set; }
		public ICommand MarkSubjectDoneCommand { get; set; }
		public ICommand RetrySubjectCommand { get; set; }
		public ICommand SetCurrentHomeworkCommand { get; set; }
		#endregion

		public SubjectViewModel(SubjectCollectionDataService _subjectCollectionDataService)
		{
			ContentToBeDisplayed = SubjectDetailDisplayType.Content;

			HomeworkForm = new HomeworkForm();
			ExamForm = new ExamForm();

			ButtonHandlerCommand = new BaseButtonCommand(ChangeChildView);
			AddHomeWorkCommand = new BaseButtonCommand(null, AddHomeWork);
			AddExamWorkCommand = new BaseButtonCommand(null, AddExam);
			MarkSubjectFailedCommand = new BaseButtonCommand(null, MarkSubjectFailed);
			MarkSubjectDoneCommand = new BaseButtonCommand(null, MarkSubjectDone);
			RetrySubjectCommand = new BaseButtonCommand(null, RetrySubject);
			SetCurrentHomeworkCommand = new BaseButtonCommand(SetHomeworkForm);

			SubjectCollectionDataService = _subjectCollectionDataService;
		}

		public void OnPageDisplay(Subject subject)
		{
			SetCurrentSubject(subject);
			ManageContentVisibility();
		}

		private void ChangeChildView(object? displayType)
		{
			if (displayType is null)
				throw new ArgumentNullException(nameof(displayType));

			SubjectDetailDisplayType type;
			Enum.TryParse(displayType.ToString(), out type);
			ContentToBeDisplayed = type;
			SetFormToEmpty();
			OnPropertyChanged(nameof(ContentToBeDisplayed));
		}

		private async void AddHomeWork()
		{
			// TODO: Add validation
			if (
				HomeworkForm!.NewHomeworkName!.Equals(null)
				|| HomeworkForm.NewHomeworkName.Equals(String.Empty)
				|| HomeworkForm.NewHomeworkDateToComplete.Equals(null)
				|| HomeworkForm.NewHomeworkDateToComplete.Equals(DateTime.MinValue)
				|| HomeworkForm.NewHomeworkMaxPoints.Equals(0)
				|| HomeworkForm!.NewHomeworkType!.Equals(null)
				|| HomeworkForm.NewHomeworkType.Equals(String.Empty)
			)
				return;

			SetIsExamButtonEnable(false);
			OnPropertyChanged(nameof(HomeworkForm));

			HomeworkType homeworkType = (HomeworkType)
				Enum.Parse(typeof(HomeworkType), HomeworkForm.NewHomeworkType.Replace("System.Windows.Controls.ComboBoxItem: ", ""));

			if (CurrentSubject?.Id == null)
				return;

			Homework newHomework =
				new()
				{
					Id = Guid.NewGuid(),
					SubjectId = CurrentSubject.Id,
					Name = HomeworkForm.NewHomeworkName,
					Type = homeworkType,
					MaxPoints = HomeworkForm.NewHomeworkMaxPoints,
					DateToComlete = (DateTime)HomeworkForm.NewHomeworkDateToComplete,
					IsCompleted = false,
					Points = 0,
				};

			// Add timeouts before adding backend
			await Task.Delay(3000);

			CurrentSubject!.Homeworks!.Add(newHomework);
			OnPropertyChanged(nameof(CurrentSubject));
			SetFormToEmpty();
			ButtonHandlerCommand.Execute(SubjectDetailDisplayType.Content);
			SetIsExamButtonEnable(true);
		}

		private async void AddExam()
		{
			// TODO: Add validation
			if (
				ExamForm.NewExamDate.Equals(null)
				|| ExamForm.NewExamDate.Equals(DateTime.MinValue)
				|| ExamForm.NewExamTime.Equals(null)
				|| ExamForm.NewExamTime.Equals(DateTime.MinValue)
			)
				return;

			SetIsExamButtonEnable(false);
			OnPropertyChanged(nameof(ExamForm));

			DateTime dateWithoutTime = (DateTime)ExamForm.NewExamDate;
			DateTime onlyTime = (DateTime)ExamForm.NewExamTime;

			DateTime combinedDateTime = new DateTime(
				dateWithoutTime.Year,
				dateWithoutTime.Month,
				dateWithoutTime.Day,
				onlyTime.Hour,
				onlyTime.Second,
				onlyTime.Minute
			);

			if (CurrentSubject?.Id == null)
				return;

			Exam newExam =
				new()
				{
					Id = Guid.NewGuid(),
					SubjectId = CurrentSubject.Id,
					Date = combinedDateTime,
					Result = Enums.ExamResult.NotYetGraded,
				};

			// Add timeouts before adding backend
			await Task.Delay(3000);

			CurrentSubject?.ExamHistory?.Add(newExam);
			OnPropertyChanged(nameof(CurrentSubject));
			SetFormToEmpty();
			ButtonHandlerCommand.Execute(SubjectDetailDisplayType.Content);
			SetIsExamButtonEnable(true);
		}

		private void SetHomeworkForm(object? id)
		{
			if (id == null)
				return;
			Guid homeworkId = (Guid)id;
			Homework? currentHomerwork = SubjectCollectionDataService.GetHomeworkById(CurrentSubject.Id, homeworkId);
			if (currentHomerwork == null)
				return;
			SetCurrentHomeworkId(currentHomerwork.Id);
			SetHomeworkFormState(HomeworkFormState.EDIT);
			ButtonHandlerCommand.Execute(SubjectDetailDisplayType.AddHomework);
		}

		private void EditHomework() { }

		private void MarkSubjectFailed()
		{
			if (CurrentSubject?.Id != null)
				SetIsFailed(true);
			ManageContentVisibility();
		}

		private void MarkSubjectDone()
		{
			if (CurrentSubject?.Id != null)
				SetIsCompleted(true);
			ManageContentVisibility();
		}

		private void RetrySubject()
		{
			if (CurrentSubject?.Id != null)
				SetIsFailed(false);
			ManageContentVisibility();
		}

		private void ManageContentVisibility()
		{
			if (CurrentSubject == null)
				return;

			if (CurrentSubject.IsFailed)
				VisibileContent = 1;

			if (CurrentSubject.IsCompleted)
				VisibileContent = 2;

			if (!CurrentSubject!.IsCompleted && !CurrentSubject!.IsFailed)
				VisibileContent = 0;

			OnPropertyChanged(nameof(CurrentSubject));
			OnPropertyChanged(nameof(VisibileContent));
		}

		private void SetFormToEmpty()
		{
			HomeworkForm.NewHomeworkName = string.Empty;
			HomeworkForm.NewHomeworkDateToComplete = null;
			HomeworkForm.NewHomeworkMaxPoints = 0;
			HomeworkForm.NewHomeworkType = string.Empty;
			ExamForm.NewExamDate = null;
			ExamForm.NewExamTime = null;
		}

		#region SETTERS
		private void SetCurrentSubject(Subject subject)
		{
			CurrentSubject = subject;
		}

		private void SetIsFailed(bool value)
		{
			if (CurrentSubject == null)
				return;
			CurrentSubject.IsFailed = value;
		}

		private void SetIsCompleted(bool value)
		{
			if (CurrentSubject == null)
				return;
			CurrentSubject.IsCompleted = value;
		}

		private void SetIsExamButtonEnable(bool value)
		{
			ExamForm.IsAddExamButtonEnabled = value;
		}

		private void SetCurrentHomeworkId(Guid value)
		{
			CurrentHomeworkId = value;
		}

		private void SetHomeworkFormState(HomeworkFormState state)
		{
			HomeworkForm.FormState = state;
		}
		#endregion
	}
}
