using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniOverview.Enums.Subjects;
using UniOverview.Enums;
using UniOverview.Models.Subjects;
using UniOverview.Models;
using UniOverview.Models.Teachers;
using System.Reflection.Metadata;
using System.Windows.Controls;

namespace UniOverview.Services.mock
{
	public class TeachersCollectionDataService
	{
		private ObservableCollection<Teacher> TeachersCollection = new();
		private Teacher? CurrentTeacher { get; set; }

		public TeachersCollectionDataService()
		{
			Teacher teacherOne = new Teacher()
			{
				Id = Guid.NewGuid(),
				Name = "Jane",
				Surname = "Doe",
				Email = "JaneDoe@test.com",
				Title = "PhD"
			};
			Teacher teacherTwo = new Teacher()
			{
				Id = Guid.NewGuid(),
				Name = "John",
				Surname = "Doe",
				Email = "JohnDoe@test.com",
				Title = "Mgr"
			};
			Teacher teacherThree = new Teacher()
			{
				Id = Guid.NewGuid(),
				Name = "Jack",
				Surname = "Smith",
				Email = "jacksmith@test.com",
				Title = "PhD"
			};

			TeachersCollection.Add(teacherOne);
			TeachersCollection.Add(teacherTwo);
			TeachersCollection.Add(teacherThree);
		}

		public void SetTeacher(Guid? id)
		{
			if (id.Equals(null))
			{
				CurrentTeacher = null;
			}
			else
			{
				Teacher teacher = TeachersCollection.First(t => t.Id == id);
				if (teacher == null)
					return;

				CurrentTeacher = teacher;
			}
		}

		public ObservableCollection<Teacher> GetTeachers()
		{
			return TeachersCollection;
		}

		public ObservableCollection<TeachersName> GetTeachersFullNames()
		{
			ObservableCollection<TeachersName> teachersNames = new ObservableCollection<TeachersName>();
			foreach (var teacher in TeachersCollection)
			{
				TeachersName teachersName = new() { Name = $"{teacher.Name} {teacher.Surname}" };

				teachersNames.Add(teachersName);
			}

			return teachersNames;
		}

		public Teacher? GetTeacher()
		{
			return CurrentTeacher;
		}

		public void AddTeacher(Teacher teacher)
		{
			TeachersCollection.Add(teacher);
		}

		public Teacher? GetTeacherByName(string fullName)
		{
			string firstname = fullName.Split(' ')[0];
			string surname = fullName.Split(' ')[1];

			Teacher? foundTeacher = TeachersCollection.FirstOrDefault(x => x.Name.Equals(firstname) && x.Surname.Equals(surname));
			return foundTeacher;
		}

		public string[] GetAllTitles()
		{
			string[] arrayOfTitles =
			{
				"Bc.",
				"BcA.",
				"Ing.",
				"Ing Arch.",
				"MUDr.",
				"MDDr.",
				"MVDr.",
				"MgA.",
				"Mgr.",
				"JUDr.",
				"JUDr.",
				"PhDr.",
				"RNDr.",
				"PharmDr.",
				"ThLic.",
				"ThDr.",
				"MSDr.",
				"PaedDr.",
				"PhMr.",
				"RCDr.",
				"RSDr.",
				"RTDr.",
				"ThMgr.",
				"Ph.D.",
				"DSc.",
				"CSc.",
				"Dr.",
				"DrSc.",
				"Th.D.",
				"odb. as.",
				"doc.",
				"prof."
			};

			return arrayOfTitles;
		}
	}
}
