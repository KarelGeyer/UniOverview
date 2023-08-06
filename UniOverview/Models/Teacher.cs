using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniOverview.Models
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? Email { get; set; }
        public string? PreNameTitle { get; set; }
        public string? AfterNameTitle { get; set; }
    }
}
