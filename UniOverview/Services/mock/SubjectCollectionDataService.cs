using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniOverview.Enums.Subjects;
using UniOverview.Enums;
using UniOverview.Models;
using UniOverview.Models.Subjects;
using UniOverview.Models.Teachers;

namespace UniOverview.Services.mock
{
	public class SubjectCollectionDataService
	{
		private ObservableCollection<Subject> SubjectsCollection = new();

		public SubjectCollectionDataService()
		{
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
				IsCompleted = true,
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
				IsFailed = true,
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

			Subject subjectThree = new Subject()
			{
				Id = Guid.NewGuid(),
				Name = "BZáklady Informatiky",
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

			subjectThree.ExamHistory.Add(exam);
			subjectThree.ExamHistory.Add(exam2);

			subjectThree.Homeworks.Add(homework);
			subjectThree.Homeworks.Add(homework2);

			SubjectsCollection.Add(subjectOne);
			SubjectsCollection.Add(subjectTwo);
			SubjectsCollection.Add(subjectThree);

			SubjectsCollection.OrderBy(x => x.Name);
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
			Subject? selectedSubject = SubjectsCollection.Where(subject => subject.Id.Equals(id)).FirstOrDefault();

			if (selectedSubject == null)
			{
				throw new Exception("Subject not found");
			}

			return selectedSubject;
		}

		public void AddSubject(Subject subject)
		{
			SubjectsCollection.Add(subject);
		}

		public ObservableCollection<Subject> GetSubjects()
		{
			return SubjectsCollection;
		}

		public Homework? GetHomeworkById(Guid subjectId, Guid homeworkId)
		{
			Subject? selectedSubject = GetSelectedSubject(subjectId);
			if (selectedSubject == null || selectedSubject.Homeworks == null)
				return null;
			Homework? homework = selectedSubject.Homeworks.Where(x => x.Id == homeworkId).FirstOrDefault();

			return homework;
		}

		#region SETTERS
		public void SetSubjects(ObservableCollection<Subject> subjects)
		{
			SubjectsCollection = subjects;
		}
		#endregion
	}
}
