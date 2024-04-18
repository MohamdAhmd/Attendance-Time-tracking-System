using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Attendance_Time_tracking_System.Models;
using Microsoft.AspNetCore.Authorization;

namespace Attendance_Time_tracking_System.Controllers
{
    // only supervisors can access this controller

    [Authorize(Roles = "Supervisor")]
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
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int id = int.Parse(userIdClaim.Value);
           return View(trackDaysRepo.GetTrackDaysBySupervisorId(id));
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
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int id = int.Parse(userIdClaim.Value);
            ViewData["TrackId"] = new SelectList(trackRepo.GetTrackBySupervisorId(id), "Id", "Name");


            ViewData["DayId"] = new SelectList(daysRepo.GetAllDays() , "Id", "Id");
            //ViewData["TrackId"] = new SelectList(trackRepo.GetAllTracks() , "Id", "Name");
            return View();
        }

        // POST: TrackDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackId,StartPeriod,Status,Lecture1,Lecture2,Lecture3")] TrackDays trackDays)
        {
            if (ModelState.IsValid)
            {
                // Check if the day exists in the database based on StartPeriod
                var day = daysRepo.GetDayByDate(trackDays.StartPeriod);

                if (day == null)
                {
                    // If the day does not exist, create a new day
                    day = new Days { Day = trackDays.StartPeriod.Date };
                    daysRepo.AddDay(day);
                }

                // Check if the schedule already exists for the selected day and track
                var existingTrackDay = trackDaysRepo.GetTrackDayById(day.Id, trackDays.TrackId);
                if (existingTrackDay != null)
                {
                    var userIdClaimm = HttpContext.User.FindFirst("UserId");
                    int idd = int.Parse(userIdClaimm.Value);
                    ViewData["TrackId"] = new SelectList(trackRepo.GetTrackBySupervisorId(idd), "Id", "Name");

                    
                    ModelState.AddModelError("", "Schedule already exists for the selected day and track.");
                    ViewData["DayId"] = new SelectList(daysRepo.GetAllDays(), "Id", "Id", trackDays.DayId);
                    //ViewData["TrackId"] = new SelectList(trackRepo.GetAllTracks(), "Id", "Name", trackDays.TrackId);
                    return View(trackDays);
                }

                trackDays.DayId = day.Id;

                trackDaysRepo.AddTrackDay(trackDays);
                return RedirectToAction(nameof(Index));
            }
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int id = int.Parse(userIdClaim.Value);
            ViewData["TrackId"] = new SelectList(trackRepo.GetTrackBySupervisorId(id), "Id", "Name");

            ViewData["DayId"] = new SelectList(daysRepo.GetAllDays(), "Id", "Id", trackDays.DayId);
           // ViewData["TrackId"] = new SelectList(trackRepo.GetAllTracks(), "Id", "Name", trackDays.TrackId);
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

            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int id = int.Parse(userIdClaim.Value);
            ViewData["TrackId"] = new SelectList(trackRepo.GetTrackBySupervisorId(id), "Id", "Name");


            ViewData["DayId"] = new SelectList(daysRepo.GetAllDays(), "Id", "Id", trackDays.DayId);
           // ViewData["TrackId"] = new SelectList(trackRepo.GetAllTracks(), "Id", "Name", trackDays.TrackId);
            return View(trackDays);
        }

        // POST: TrackDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? dayId, int? trackId, [Bind("DayId,TrackId,StartPeriod,Status,Lecture1,Lecture2,Lecture3")] TrackDays trackDays)
        {
            if (dayId != trackDays.DayId || trackId != trackDays.TrackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var day = daysRepo.GetDayByDate(trackDays.StartPeriod);

                    if (day == null)
                    {
                        // If the day does not exist, create a new day
                        day = new Days { Day = trackDays.StartPeriod.Date };
                        daysRepo.AddDay(day);
                        // Update the trackDays object with the newly created dayId
                        //trackDays.DayId = day.Id;
                    }

                    // Check if the selected day already has a schedule for the same track
                    var existingTrackDay = trackDaysRepo.GetTrackDayById(day.Id, trackDays.TrackId);
                    if (existingTrackDay != null && existingTrackDay.DayId != dayId)
                    {
                        var userIdClaim = HttpContext.User.FindFirst("UserId");
                        int id = int.Parse(userIdClaim.Value);
                        ViewData["TrackId"] = new SelectList(trackRepo.GetTrackBySupervisorId(id), "Id", "Name");

                        ModelState.AddModelError("", "Schedule already exists for the selected day and track.");
                        ViewData["DayId"] = new SelectList(daysRepo.GetAllDays(), "Id", "Id", trackDays.DayId);
                        //ViewData["TrackId"] = new SelectList(trackRepo.GetAllTracks(), "Id", "Name", trackDays.TrackId);
                        return View(trackDays);
                    }

                    trackDaysRepo.UpdateTrackDay(trackDays,day.Id);
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
            var userIdClaimm = HttpContext.User.FindFirst("UserId");
            int idd = int.Parse(userIdClaimm.Value);
            ViewData["TrackId"] = new SelectList(trackRepo.GetTrackBySupervisorId(idd), "Id", "Name");
            ViewData["DayId"] = new SelectList(daysRepo.GetAllDays(), "Id", "Id", trackDays.DayId);
           // ViewData["TrackId"] = new SelectList(trackRepo.GetAllTracks(), "Id", "Name", trackDays.TrackId);
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
