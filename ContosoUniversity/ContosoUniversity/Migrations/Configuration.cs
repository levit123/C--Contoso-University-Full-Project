namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using ContosoUniversity.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ContosoUniversity.DAL.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //This seed method allows us to insert or update test data when creating or updating the database
        //with Code First. This method is automatically called when the database is created, and whenever
        //the database schema is changed when we edit a model
        protected override void Seed(ContosoUniversity.DAL.SchoolContext context)
        {
            //creates and defines a list of Student objects
            var students = new List<Student>
            {
                new Student { FirstName = "Carson", LastName = "Alexander", EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { FirstName = "Meredith", LastName = "Alonso", EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Arturo", LastName = "Anand", EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Ezekiel", LastName = "Matthews", EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "James", LastName = "Woodrow", EnrollmentDate = DateTime.Parse("2009-09-01") }
            };

            //adds the list of Student objects to the context for the database, or updates their info in the context if they already exist
            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            //creates and defines a list of Course objects
            var courses = new List<Course>
            {
                new Course { CourseID = 1050, Title = "Chemistry", Credits = 3 },
                new Course { CourseID = 4022, Title = "Microeconomics", Credits = 3 },
                new Course { CourseID = 4041, Title = "Macroeconomics", Credits = 3 },
                new Course { CourseID = 1045, Title = "Calculus", Credits = 4 },
                new Course { CourseID = 3141, Title = "Trigonometry", Credits = 4 },
                new Course { CourseID = 2021, Title = "Composition", Credits = 3 },
                new Course { CourseID = 2042, Title = "Literature", Credits = 4 }
            };

            //adds the list of Course objects to the context for the database, or updates their info in the context if they already exist
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();

            //creates a list of Enrollment objects, which holds info that ties the students to the courses
            var enrollments = new List<Enrollment>
            {
                //uses lambda statements to grab the student's ID's and the course's ID's values and
                //saves them to the properties of the Enrollment objects
                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                    Grade = Grade.A
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                    Grade = Grade.C
                },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                    Grade = Grade.B
                 },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Matthews").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Woodrow").ID,
                    CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Woodrow").ID,
                    CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                    Grade = Grade.B
                 }
            };

            foreach (Enrollment e in enrollments)
            {
                //grabs the Enrollment object in the database context that have matching info between the
                //Enrollment, the Student, and the Course
                var enrollmentInDatabase = context.Enrollments.Where(
                    s =>
                        s.Student.ID == e.StudentID &&
                        s.Course.CourseID == e.CourseID).SingleOrDefault();
                //if the current Enrollment in the database context is null, then adds the current object from the "enrollments"
                //list to the database context
                if (enrollmentInDatabase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}
