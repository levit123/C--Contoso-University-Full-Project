using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ContosoUniversity.Models;

namespace ContosoUniversity.DAL
{
    //class that initializes the database with default starting data
    public class SchoolInitializer : System.Data.Entity. DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            //creates a list of Student objects and defines the info for each Student object
            var students = new List<Student>
            {
                new Student{FirstName = "Anna", LastName = "Belle", EnrollmentDate = DateTime.Parse("1950-03-01") },
                new Student{FirstName = "Carson", LastName = "Bailey", EnrollmentDate = DateTime.Parse("1948-05-01") },
                new Student{FirstName = "Elizabeth", LastName = "Comstock", EnrollmentDate = DateTime.Parse("1952-06-01") },
                new Student{FirstName = "Andrew", LastName = "Ryan", EnrollmentDate = DateTime.Parse("1945-03-01")},
                new Student{FirstName = "Madd", LastName = "Dock", EnrollmentDate = DateTime.Parse("1948-08-01")},
                new Student{FirstName = "Frank", LastName = "Fontaine", EnrollmentDate = DateTime.Parse("1946-10-01")}
            };

            //iterates through the list of students and adds them to the school database context
            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            //creates a list of Course objects and defines the info for each Course object
            var courses = new List<Course>
            {
                new Course{CourseID = 101, Title = "Philosophy", Credits = 3},
                new Course{CourseID = 155, Title = "Basic Economics", Credits = 4 },
                new Course{CourseID = 201, Title = "Harvesting And Processing of Adam", Credits = 3 },
                new Course{CourseID = 255, Title = "Basic Chemistry", Credits = 5},
                new Course{CourseID = 301, Title = "First Aid", Credits = 3 },
                new Course{CourseID = 351, Title = "Cosmetic Surgery", Credits = 5 }
            };

            //iterates through the list of courses and adds them to the school database context
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            //creates a list of Enrollment objects and defines the info for each Enrollment object
            var enrollments = new List<Enrollment>
            {
                new Enrollment{StudentID = 1, CourseID = 201, Grade = Grade.A},
                new Enrollment{StudentID = 2, CourseID = 255, Grade = Grade.B},
                new Enrollment{StudentID = 3, CourseID = 301, Grade = Grade.A},
                new Enrollment{StudentID = 4, CourseID = 101, Grade = Grade.C},
                new Enrollment{StudentID = 5, CourseID = 351, Grade = Grade.C},
                new Enrollment{StudentID = 6, CourseID = 155, Grade = Grade.D}
            };

            //iterates through the list of Enrollment objects and adds them to the school database context
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}