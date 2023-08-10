using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniOverview.Commands;
using UniOverview.Enums.Subjects;
using UniOverview.Models.Subjects;

namespace UniOverview.ViewModels.Subjects
{
	public class SubjectViewModel : ViewModelBase
	{
		#region Private Properties
		private Subject currentSubject { get; set; }
		#endregion

		#region Public Properties
		public Subject CurrentSubject => currentSubject;
		public SubjectDetailDisplayType ContentToBeDisplayed { get; set; }
		public HomeworkForm HomeworkForm { get; set; } = new();
		public ExamForm ExamForm { get; set; } = new();
		#endregion

		#region Commands
		public ICommand ButtonHandlerCommand { get; set; }
		public ICommand AddHomeWorkCommand { get; set; }
		public ICommand AddExamWorkCommand { get; set; }
		#endregion

		public SubjectViewModel()
		{
			ContentToBeDisplayed = SubjectDetailDisplayType.Content;
			ButtonHandlerCommand = new BaseButtonCommand(ChangeChildView);
			AddHomeWorkCommand = new BaseButtonCommand(null, AddHomeWork);
			AddExamWorkCommand = new BaseButtonCommand(null, AddExam);
		}

		public void SetCurrentSubject(Subject subject)
		{
			currentSubject = subject;
		}

		public void ChangeChildView(object? displayType)
		{
			if (displayType is null)
				throw new ArgumentNullException(nameof(displayType));

			SubjectDetailDisplayType type;
			Enum.TryParse(displayType.ToString(), out type);
			ContentToBeDisplayed = type;
			SetFormToEmpty();
			OnPropertyChanged(nameof(ContentToBeDisplayed));
		}

		public async void AddHomeWork()
		{
			// TODO: Add validation
			if (HomeworkForm!.NewHomeworkName!.Equals(null) || HomeworkForm.NewHomeworkName.Equals(String.Empty))
			{
				return;
			}

			if (
				HomeworkForm.NewHomeworkDateToComplete.Equals(null)
				|| HomeworkForm.NewHomeworkDateToComplete.Equals(DateTime.MinValue)
			)
			{
				return;
			}

			if (HomeworkForm.NewHomeworkMaxPoints.Equals(0))
			{
				return;
			}

			if (HomeworkForm!.NewHomeworkType!.Equals(null) || HomeworkForm.NewHomeworkType.Equals(String.Empty))
			{
				return;
			}

			HomeworkForm.IsAddHomeworkButtonEnabled = false;
			OnPropertyChanged(nameof(HomeworkForm));

			HomeworkType homeworkType = (HomeworkType)
				Enum.Parse(
					typeof(HomeworkType),
					HomeworkForm.NewHomeworkType.Replace("System.Windows.Controls.ComboBoxItem: ", "")
				);

			Homework newHomework =
				new()
				{
					Id = Guid.NewGuid(),
					SubjectId = currentSubject.Id,
					Name = HomeworkForm.NewHomeworkName,
					Type = homeworkType,
					MaxPoints = HomeworkForm.NewHomeworkMaxPoints,
					DateToComlete = (DateTime)HomeworkForm.NewHomeworkDateToComplete,
					IsCompleted = false,
					Points = 0,
				};

			// Add timeouts before adding backend
			await Task.Delay(3000);

			currentSubject!.Homeworks!.Add(newHomework);
			OnPropertyChanged(nameof(currentSubject));
			SetFormToEmpty();
			ButtonHandlerCommand.Execute(SubjectDetailDisplayType.Content);
			HomeworkForm.IsAddHomeworkButtonEnabled = true;
		}

		public async void AddExam()
		{
			// TODO: Add validation
			if (ExamForm.NewExamDate.Equals(null) || ExamForm.NewExamDate.Equals(DateTime.MinValue))
			{
				return;
			}

			if (ExamForm.NewExamTime.Equals(null) || ExamForm.NewExamTime.Equals(DateTime.MinValue))
			{
				return;
			}

			ExamForm.IsAddExamButtonEnabled = false;
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

			Exam newExam =
				new()
				{
					Id = Guid.NewGuid(),
					SubjectId = currentSubject.Id,
					Date = combinedDateTime,
					Result = Enums.ExamResult.NotYetGraded,
				};

			// Add timeouts before adding backend
			await Task.Delay(3000);

			currentSubject.ExamHistory.Add(newExam);
			OnPropertyChanged(nameof(currentSubject));
			SetFormToEmpty();
			ButtonHandlerCommand.Execute(SubjectDetailDisplayType.Content);
			ExamForm.IsAddExamButtonEnabled = true;
		}

		public void SetFormToEmpty()
		{
			HomeworkForm.NewHomeworkName = string.Empty;
			HomeworkForm.NewHomeworkDateToComplete = null;
			HomeworkForm.NewHomeworkMaxPoints = 0;
			HomeworkForm.NewHomeworkType = string.Empty;
			ExamForm.NewExamDate = null;
			ExamForm.NewExamTime = null;
		}
	}
}
