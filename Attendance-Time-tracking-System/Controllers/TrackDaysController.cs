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
            
            return View(trackDaysRepo.GetAllTrackDays());
        }

        // GET: TrackDays/Details/5
        public async Task<IActionResult> Details(int? dayId, int? trackId)
        {
            if(dayId == null || trackId == null)
            {
                return NotFound();
            }
            var trackDays = trackDaysRepo.GetTrackDayById(dayId.Value, trackId.Value);
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
                trackDaysRepo.AddTrackDay(trackDays);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DayId"] = new SelectList(daysRepo.GetAllDays(), "Id", "Id", trackDays.DayId);
            ViewData["TrackId"] = new SelectList(trackRepo.GetAllTracks(), "Id", "Name", trackDays.TrackId);
            return View(trackDays);
        }

        // GET: TrackDays/Edit/5
        public async Task<IActionResult> Edit(int? dayId, int? trackId)
        {
            
            if (dayId == null || trackId == null)
            {
                return NotFound();
            }
            var trackDays = trackDaysRepo.GetTrackDayById(dayId.Value, trackId.Value);
            if (trackDays == null)
            {
                return NotFound();
            }
            ViewData["DayId"] = new SelectList(daysRepo.GetAllDays(), "Id", "Id", trackDays.DayId);
            ViewData["TrackId"] = new SelectList(trackRepo.GetAllTracks(), "Id", "Name", trackDays.TrackId);
            return View(trackDays);
        }

        // POST: TrackDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int dayId, int trackId, [Bind("DayId,TrackId,StartPeriod,Status,Lecture1,Lecture2,Lecture3")] TrackDays trackDays)
        {
            if (dayId != trackDays.DayId || trackId != trackDays.TrackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    trackDaysRepo.UpdateTrackDay(trackDays);
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
            ViewData["DayId"] = new SelectList(daysRepo.GetAllDays(), "Id", "Id", trackDays.DayId);
            ViewData["TrackId"] = new SelectList(trackRepo.GetAllTracks(), "Id", "Name", trackDays.TrackId);
            return View(trackDays);
        }
        // GET: TrackDays/Delete/5
        public async Task<IActionResult> Delete(int? dayId, int? trackId)
        {    
            if (dayId == null || trackId == null)
            {
                return NotFound();
            }
   
            var trackDays = trackDaysRepo.GetTrackDayById(dayId.Value, trackId.Value);
            if (trackDays == null)
            {
                return NotFound();
            }

            return View(trackDays);
        }

        // POST: TrackDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int dayId, int trackId)
        {
            var trackDays = trackDaysRepo.GetTrackDayById(dayId, trackId);
            if (trackDays != null)
            {
                trackDaysRepo.DeleteTrackDay(dayId);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TrackDaysExists(int id)
        {
            return trackDaysRepo.GetAllTrackDays().Any(e => e.DayId == id);
        }
    }
}
