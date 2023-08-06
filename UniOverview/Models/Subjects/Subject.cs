using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniOverview.Enums;
using UniOverview.Enums.Subjects;

namespace UniOverview.Models.Subjects
{
	public class Subject
	{
		public required Guid Id { get; set; }
		public required string Name { get; set; }
		public string? ShortName { get; set; }
		public bool IsCompleted { get; set; }
		public bool IsFailed { get; set; }
		public int Credits { get; set; }
		public int Points { get; set; }
		public string? SubjectTypeIcon { get; set; }
		public string? SubjectTypeColor { get; set; }
		public Teacher? Teacher { get; set; }
		public required List<Exam> ExamHistory { get; set; }
		public List<Homework>? Homeworks { get; set; }
		public Grade? Grade { get; set; }
		public SubjectType? SubjectType { get; set; }
		public TypeOfCompletion? TypeOfCompletion { get; set; }
		public FormOfCompletion? FormOfCompletion { get; set; }
	}
}
