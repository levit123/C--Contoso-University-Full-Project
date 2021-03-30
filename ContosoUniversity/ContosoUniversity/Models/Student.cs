using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        //[StringLength] Limits the length of the string property to 50 characters max.
        //NOTE: this does not restrict the user from inputting white space into the name. If you
        //wanted to restrict what kind of characters they can input, you would use "RegularExpression".
        //For example, this would limits the characters to be only alphabetical, and requires
        //the first character to be uppercase:
        //
        //[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        //makes the FirstName field required
        [Required]
        //Limits the string to be 50 characters max, and specifies a custom message for the error
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
        //specifies that when the database is created, the column of the Student table that maps to this model property will be named "FirstName"
        [Column("FirstName")]
        //customizes/sets the caption for the text box
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        //Specifies that we only want to keep track of just the date, not the date and time
        [DataType(DataType.Date)]
        //specifies the format for the date, and specifies that the formatting should be applied
        //when the user is editing the value in a text box
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        //customizes/sets the caption for the text box
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}