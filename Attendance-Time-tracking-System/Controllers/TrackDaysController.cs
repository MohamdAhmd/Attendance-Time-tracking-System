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
    public class TrackDaysController : Controller
    {
        ITrackDaysRepo trackDaysRepo;
        IDaysRepo daysRepo;
        ITrackRepo trackRepo;
        public TrackDaysController(ITrackDaysRepo trackDaysRepo, IDaysRepo daysRepo, ITrackRepo trackRepo)
        {
            this.trackDaysRepo = trackDaysRepo;
            this.daysRepo = daysRepo;
            this.trackRepo = trackRepo;
        }

        // GET: TrackDays
        public async Task<IActionResult> Index()
        {
            
            return View(trackDaysRepo.GetAllDays());
        }

        // GET: TrackDays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           var trackDays = trackDaysRepo.GetDayById(id.Value);
            if (trackDays == null)
            {
                return NotFound();
            }

            return View(trackDays);
        }

        // GET: TrackDays/Create
        public IActionResult Create()
        {
         
            ViewData["DayId"] = new SelectList(daysRepo.GetAllDays() , "Id", "Id");
            ViewData["TrackId"] = new SelectList(trackRepo.GetAllTracks() , "Id", "Name");
            return View();
        }

        // POST: TrackDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DayId,TrackId,StartPeriod,Status,Lecture1,Lecture2,Lecture3")] TrackDays trackDays)
        {
            if (ModelState.IsValid)
            {
                trackDaysRepo.AddDay(trackDays);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["DayId"] = new SelectList(_context.Days, "Id", "Id", trackDays.DayId);
            //ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Name", trackDays.TrackId);
            ViewData["DayId"] = new SelectList(daysRepo.GetAllDays(), "Id", "Id", trackDays.DayId);
            ViewData["TrackId"] = new SelectList(trackRepo.GetAllTracks(), "Id", "Name", trackDays.TrackId);
            return View(trackDays);
        }

        // GET: TrackDays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackDays = trackDaysRepo.GetDayById(id.Value);
            if (trackDays == null)
            {
                return NotFound();
            }
            //ViewData["DayId"] = new SelectList(_context.Days, "Id", "Id", trackDays.DayId);
            //ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Name", trackDays.TrackId);
            ViewData["DayId"] = new SelectList(daysRepo.GetAllDays(), "Id", "Id", trackDays.DayId);
            ViewData["TrackId"] = new SelectList(trackRepo.GetAllTracks(), "Id", "Name", trackDays.TrackId);
            return View(trackDays);
        }

        // POST: TrackDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DayId,TrackId,StartPeriod,Status,Lecture1,Lecture2,Lecture3")] TrackDays trackDays)
        {
            if (id != trackDays.DayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    trackDaysRepo.UpdateDay(trackDays);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackDaysExists(trackDays.DayId))
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
            //ViewData["DayId"] = new SelectList(_context.Days, "Id", "Id", trackDays.DayId);
            //ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Name", trackDays.TrackId);
            ViewData["DayId"] = new SelectList(daysRepo.GetAllDays(), "Id", "Id", trackDays.DayId);
            ViewData["TrackId"] = new SelectList(trackRepo.GetAllTracks(), "Id", "Name", trackDays.TrackId);
            return View(trackDays);
        }

        // GET: TrackDays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                
            var trackDays = trackDaysRepo.GetDayById(id.Value);
            if (trackDays == null)
            {
                return NotFound();
            }

            return View(trackDays);
        }

        // POST: TrackDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var trackDays = trackDaysRepo.GetDayById(id);
            if (trackDays != null)
            { 
                trackDaysRepo.DeleteDay(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TrackDaysExists(int id)
        {
            return trackDaysRepo.GetDayById(id) != null;
        }
    }
}
