using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Attendance_Time_tracking_System.Models;

namespace Attendance_Time_tracking_System.Controllers
{
    
    public class DaysController : Controller
    {
        IDaysRepo _daysRepo;

        public DaysController(IDaysRepo daysRepo)
        {
            _daysRepo = daysRepo;
        }

        // GET: Days
        public async Task<IActionResult> Index()
        {
            return View(_daysRepo.GetAllDays());
        }

        // GET: Days/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
              var days = _daysRepo.GetDayById(id.Value);
            if (days == null)
            {
                return NotFound();
            }

            return View(days);
        }

        // GET: Days/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Days/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Day")] Days days)
        {
            if (ModelState.IsValid)
            {
                _daysRepo.AddDay(days);
                return RedirectToAction(nameof(Index));
            }
            return View(days);
        }

        // GET: Days/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var days = _daysRepo.GetDayById(id.Value);
            if (days == null)
            {
                return NotFound();
            }
            return View(days);
        }

        // POST: Days/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Day")] Days days)
        {
            if (id != days.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _daysRepo.UpdateDay(days);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DaysExists(days.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(days);
        }

        // GET: Days/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var days = _daysRepo.GetDayById(id.Value);
            if (days == null)
            {
                return NotFound();
            }

            return View(days);
        }

        // POST: Days/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var days = _daysRepo.GetDayById(id);
            if (days != null)
            {
                _daysRepo.DeleteDay(id);
            }

            
            return RedirectToAction(nameof(Index));
        }

        private bool DaysExists(int id)
        {
            return  _daysRepo.GetDayById(id) != null;
        }
    }
}
