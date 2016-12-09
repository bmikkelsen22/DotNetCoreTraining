using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dal;
using Dal.Models;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    public class PeopleController : Controller
    {
        private BusinessProContext db = new BusinessProContext();
        // GET: /<controller>/
        public IActionResult Index()
        {
            var people = db.People.ToList();
            foreach (var p in people)
            {
                if (p.RoleId!=null)
                    p.Role = db.Roles.Find(p.RoleId);
            }
            return View(people);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var person = db.People.Find(id);
            if (person == null)
                return NotFound();
            var personDepts = db.PersonDepartment.Where(pd => pd.PersonId == id);
            foreach(var pd in personDepts)
            {
                pd.Department = db.Departments.Find(pd.DepartmentId);
                person.PersonDepartments.Add(pd);
            }
            if (person.RoleId != null)
                person.Role = db.Roles.Find(person.RoleId);
            return View(person);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var person = db.People.Find(id);
            if (person == null)
                return NotFound();
            ViewBag.Roles = db.Roles.ToList();
            ViewBag.Departments = db.Departments.ToList();
            var personDepts = db.PersonDepartment.Where(pd => pd.PersonId == id);
            foreach (var pd in personDepts)
            {
                pd.Department = db.Departments.Find(pd.DepartmentId);
                person.PersonDepartments.Add(pd);
            }
            return View(person);
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        [HttpPost]
        public IActionResult AddPD(PersonDepartment pd)
        {
            if (pd == null)
                return BadRequest();
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Invalid state!");
                db.PersonDepartment.Add(pd);
                db.SaveChanges();
                pd.Department = db.Departments.Find(pd.DepartmentId);

                return PartialView("_PersonDepartmentRow", pd);
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public IActionResult DeletePD(PersonDepartment pd)
        {
            if (pd == null)
                return BadRequest();
            db.PersonDepartment.Remove(pd);
            db.SaveChanges();
            var model = db.PersonDepartment.Where(persondept => persondept.PersonId == pd.PersonId);
            foreach (var personDept in model)
            {
                personDept.Department = db.Departments.Find(personDept.DepartmentId);
            }
            return PartialView("_PersonDepartmentTable", model.ToArray());
        }
    }
}
