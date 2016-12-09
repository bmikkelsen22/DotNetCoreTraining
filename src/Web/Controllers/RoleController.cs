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
    public class RoleController : Controller
    {
        private BusinessProContext db = new BusinessProContext();

        // GET: /<controller>/
        public IActionResult Index()
        {
            var model = db.Roles.ToList();
            return View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var role=db.Roles.Find(id);
            if (role == null)
                return NotFound();
            role.People = db.People.Where(p => p.RoleId == id).ToList();
            return View(role);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var role = db.Roles.Find(id);
            if (role == null)
                return NotFound();
            return View(role);
        }

        [HttpPost]
        public IActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        [HttpPost]
        public IActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }
    }
}
