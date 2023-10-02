using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniOverview.Enums.Subjects;

namespace UniOverview.Models.Subjects
{
	public class AddNewSubject { }

	public class CompletionDetails
	{
		public SubjectType TypeOfSubject { get; set; }
		public TypeOfCompletion SubjectTypeOfCompletion { get; set; }
		public FormOfCompletion SubjectFormOfCompletion { get; set; }
	}
}
