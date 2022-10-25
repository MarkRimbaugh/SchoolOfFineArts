using System.ComponentModel.DataAnnotations;

namespace SchoolOfFineArtsModels
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        //public string MiddleName { get; set; }
        [Required, Range(1,130)]
        public int Age { get; set; }

        // Override = to take in an object and compare

        public override string ToString()
        {
            return $"Teacher ID: {Id}, First Name: {FirstName}, Last Name: {LastName}";
        }        
    }
}