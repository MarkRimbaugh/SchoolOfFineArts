using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOfFineArtsModels.DTOs
{
    public class CourseInfoDTO
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;


    }
}
