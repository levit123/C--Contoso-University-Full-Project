using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Students
        public ActionResult Index(string sortOrder)
        {
            //defines how the view/webpage should sort Students, such as sorting by name or date
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            //grabs all the students in the database and saves them to a variable named "students"
            var students = from s in db.Students select s;
            
            //determines how the user chose to sort the Students on the view/webpage, and handles their choice accordingly
            switch (sortOrder)
            {
                //if the user chose to sort by name, orders the items in the "students" variable by last name in descending order
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                //if the user chose to sort by date, orders the items in the "students" variable by enrollment date in ascending order
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                //if the user chose to sort by date, orders the items in "students" by enrollment date in descending order
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                //unless specified otherwise, sorts students by last name in ascending order
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            //after sorting the students, displays the webpage with the students in the newly sorted order
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            //if the ID passed into this method is null, displays an error page
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //attempts to locate the Student object in the database with the matching ID property, and save it to a new Student object
            Student student = db.Students.Find(id);
            //if it is unable to locate a student with the matching ID, the Student object will be null, and it will display a webpage saying it could not be found
            if (student == null)
            {
                return HttpNotFound();
            }
            //in the end, if it is able to locate a student with a matching ID in the database and save it to a Student object, it will display the Details page for that Student object
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstName,EnrollmentDate")] Student student)
        {
            //attempts to add the student to the database
            try
            {
                if (ModelState.IsValid)
                {
                    //adds the student to the Students table in the database, saves the changes made to the database, then displays the Students/Index page
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            //handles any DataExcepton errors that occur
            catch (DataException dex)
            {
                //logs a standard error message
                ModelState.AddModelError("", "Unable to save changes. Try again later, and if the problem persists, contact your system administrator.");
                //logs the error message for the specific error that occurred in this instance
                ModelState.AddModelError("", dex.Message);
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // I updated the original Edit method and changed it to this, to prevent overposting. The reason I removed the "Bind" attribute (the attribute that binds the info passed in
        // to a Student object) is because the "Bind" attribute actually clears out any pre-existing values in the object it makes. Because it clears that out, invalid info could
        // potentially replace previously existing valid info for the object it's attempting to edit. So, I removed the "Bind" attribute and instead put in safeguards such as
        // "TryUpDateModel" and a "try-catch" to make the process of editing and updating a Student object in the database more secure.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            //if the ID passed into this method is null, displays an error page
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //attempts to locate the Student object in the database with the matching ID, and saves the object a new variable
            var studentToUpdate = db.Students.Find(id);
            //attempts to update the Student in the database with the new values
            if (TryUpdateModel(studentToUpdate, "", new string[] { "LastName", "FirstName", "EnrollmentDate" }))
            {
                //attempts to save the changes made to the Student item in the database, and then display the "Student/Index" page
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                //handles any DataException errors that may occur
                catch (DataException dex)
                {
                    //logs a standard error message
                    ModelState.AddModelError("", "Unable to save changes. Try again later, and if the problem persists, contact your system administrator.");
                    //logs the error message for the specific error that occurred in this instance
                    ModelState.AddModelError("", dex.Message);
                }
            }

            return View(studentToUpdate);
        }

        // GET: Students/Delete/5
        // I  updated this Delete method to accomodate for the possibility of a faulty attempt at deleting an item in the database.
        // In the event it encounters an error when trying to delete an object in the database, it will not go through with the attempy, and instead display a page with
        // an error message and give the user the choice to either cancel or try again.
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            //if the ID passed into this method is null, displays an error page
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if the "saveChangesError" variable passed into this method is true (which indicates an error has occurred), then display an error message saying the attempt
            // to delete the object has failed
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists, contact your system administrator.";
            }
            //attempts to locate the Student object in the database with the matching ID
            Student student = db.Students.Find(id);
            //if the student is not found, display am error page saying so
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        // This used to be named "DeleteConfirmed", but was changed to just "Delete". Performs the actual delete operation. This method is technically called in conjuction
        // with the other "Delete" method above this one
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            //attempts to delete the Student object
            try
            {
                //locates the Student object in the database with the matching ID
                Student student = db.Students.Find(id);
                //Removes the specified student from the Students table in the database
                db.Students.Remove(student);
                //saves the changes made to the database
                db.SaveChanges();
            }
            //handles any DataException errors that may occur
            catch (DataException)
            {
                //displays the Delete page with the appropriate error
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
