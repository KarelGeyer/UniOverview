using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniOverview.Enums.Subjects;

namespace UniOverview.Models.Subjects
{
	public class HomeworkForm
	{
		public string? NewHomeworkName { get; set; }
		public DateTime? NewHomeworkDateToComplete { get; set; }
		public int NewHomeworkMaxPoints { get; set; }
		public string? NewHomeworkType { get; set; }
		public bool IsAddHomeworkButtonEnabled { get; set; } = true;
		public int HomeworkPoints { get; set; }
		public HomeworkFormState FormState { get; set; } = HomeworkFormState.ADD;
	}
}
