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
    public class RoomController : Controller
    {
        private BusinessProContext db = new BusinessProContext();

        // GET: /<controller>/
        public IActionResult Index()
        {
            var rooms = from r in db.Rooms
                        select r;
            return View(rooms);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var room = db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }
            room.Departments = db.Departments.Where(d => d.RoomId == id).ToList();
            return View(room);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            Room room = db.Rooms.Find(id);
            if (room == null)
                return NotFound();
            return View(room);
        }

        //post create
        [HttpPost]
        public IActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }

        //post edit
        [HttpPost]
        public IActionResult Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }

        //post delete
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Rooms.Remove(db.Rooms.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
