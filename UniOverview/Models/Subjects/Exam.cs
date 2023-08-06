using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniOverview.Enums;
using UniOverview.Enums.Subjects;

namespace UniOverview.Models.Subjects
{
    public class Exam
    {
        public required Guid Id { get; set; }
        public required int SubjectId { get; set; }
        public int Points { get; set; }
        public DateTime Date { get; set; }
        public ExamResult Result { get; set; }
        public Grade? Grade { get; set; }
    }
}
