﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOfFineArtsModels
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        public virtual List<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

        public string FriendlyName => $"{FirstName} {LastName}";

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
