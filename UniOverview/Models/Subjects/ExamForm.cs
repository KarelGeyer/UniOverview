using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniOverview.Enums;

namespace UniOverview.Models.Subjects
{
	public class ExamForm
	{
		public DateTime? NewExamDate { get; set; }
		public DateTime? NewExamTime { get; set; }
		public bool IsAddExamButtonEnabled { get; set; } = true;
	}
}
