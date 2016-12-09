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
    public class DepartmentController : Controller
    {
        private BusinessProContext db = new BusinessProContext();
        // GET: /<controller>/
        public IActionResult Index()
        {
            var depts = from d in db.Departments
                        select d;
            foreach (var d in depts)
            {
                d.Room = db.Rooms.Find(d.RoomId);
            }
            return View(depts);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var dept = db.Departments.Find(id);
            if (dept == null)
                return NotFound();
            var personDepts = db.PersonDepartment.Where(pd => pd.DepartmentId == id);
            foreach (var pd in personDepts)
            {
                pd.Person = db.People.Find(pd.PersonId);
                dept.PersonDepartments.Add(pd);
            }
            dept.Room = db.Rooms.Find(dept.RoomId);
            return View(dept);
        }

        public IActionResult Create()
        {
            ViewBag.Rooms = db.Rooms;
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Department dept = db.Departments.Find(id);
            if (dept == null)
            {
                return NotFound();
            }

            ViewBag.Rooms = db.Rooms.ToList();
            var personDepts = db.PersonDepartment.Where(pd => pd.DepartmentId == id);
            foreach (var pd in personDepts)
            {
                pd.Person = db.People.Find(pd.PersonId);
                dept.PersonDepartments.Add(pd);
            }
            ViewBag.People = db.People.ToList();
            return View(dept);
        }

        //post create
        [HttpPost]
        public IActionResult Create(Department dept)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(dept);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dept);
        }

        //post edit
        [HttpPost]
        public IActionResult Edit(Department dept)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dept).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dept);
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
                pd.Person = db.People.Find(pd.PersonId);

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
            var model = db.PersonDepartment.Where(persondept => persondept.DepartmentId == pd.DepartmentId);
            foreach(var personDept in model)
            {
                personDept.Person = db.People.Find(personDept.PersonId);
            }
            return PartialView("_PersonDepartmentTable", model.ToArray());
        }
    }
}
