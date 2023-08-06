using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UniOverview.Enums;
using UniOverview.Enums.Subjects;
using UniOverview.Models;
using UniOverview.Models.Subjects;

namespace UniOverview.ViewModels.Subjects
{
	public class SubjectsBaseViewModel : ViewModelBase
	{
		private readonly ObservableCollection<Subject> _subjectsCollection = new();
		public IEnumerable<Subject> Subjects => _subjectsCollection;
		public Subject? SelectedSubject { get; set; }

		public SubjectsBaseViewModel()
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

			_subjectsCollection.Add(subjectOne);
			_subjectsCollection.Add(subjectOne);
			_subjectsCollection.Add(subjectOne);
			_subjectsCollection.Add(subjectOne);
			_subjectsCollection.Add(subjectOne);
			_subjectsCollection.Add(subjectOne);
			SelectedSubject = subjectOne;
		}

		public string GetSubjectTypeIcon(SubjectType subjectType)
		{
			if (subjectType == SubjectType.A)
				return nameof(SubjectTypeIcon.AlphaA);
			if (subjectType == SubjectType.B)
				return nameof(SubjectTypeIcon.AlphaB);

			return nameof(SubjectTypeIcon.AlphaC);
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
	}
}
