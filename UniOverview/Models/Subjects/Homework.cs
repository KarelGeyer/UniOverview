using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniOverview.Enums.Subjects;

namespace UniOverview.Models.Subjects
{
    public class Homework
    {
        public required Guid Id { get; set; }
        public required int SubjectId { get; set; }
        public required string Name { get; set; }
        public HomeworkType Type { get; set; }
        public DateTime DateToComlete { get; set; }
    }
}
