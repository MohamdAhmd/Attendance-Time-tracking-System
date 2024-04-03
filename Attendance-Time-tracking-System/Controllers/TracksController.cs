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
   
    public class TracksController : Controller
    {
        ITrackRepo trackRepo;
        IProgramRepo programRepo;
        public TracksController(ITrackRepo trackRepo, IProgramRepo programRepo)
        {
            this.trackRepo = trackRepo;
            this.programRepo = programRepo;
        }
        // GET: Tracks
        public IActionResult Index()
        {
            return View(trackRepo.GetAllTracks());
        }

        // GET: Tracks/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var track =  trackRepo.GetTrackById(id.Value);
            if (track == null)
            {
                return NotFound();
            }
            return View(track);
        }

        // GET: Tracks/Create
        public IActionResult Create()
        {
            ViewData["SupervisorID"] = new SelectList(trackRepo.GetAllInstructors(), "Id", "Email");
            ViewData["ProgramID"] = new SelectList(programRepo.GetAllPrograms(), "Id", "Name");
            return View();
        }

        // POST: Tracks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create([Bind("Id,Name,Status,SupervisorID,ProgramID,Capacity")] Track track)
        {
            //FIX CODE HERE ===============================================================================
            if (!ModelState.IsValid)
            {
                ViewData["SupervisorID"] = new SelectList(trackRepo.GetAllInstructors(), "Id", "Email");
                ViewData["ProgramID"] = new SelectList(programRepo.GetAllPrograms(), "Id", "Name");
                return View("create", track);
            }
            trackRepo.AddTrack(track);
            return RedirectToAction("index");
        }

        // GET: Tracks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var track = trackRepo.GetTrackById(id.Value); 
            if (track == null)
            {
                return NotFound();
            }
            ViewData["SupervisorID"] = new SelectList(trackRepo.GetAllInstructors(), "Id", "Email");
            ViewData["ProgramID"] = new SelectList(programRepo.GetAllPrograms(), "Id", "Name");
            return View(track);
        }

        // POST: Tracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Status,SupervisorID,ProgramID,Capacity")] Track track)
        {
            if (id != track.Id)
            {
                return NotFound();
            }
            //FIX CODE HERE ===============================================================================
            if (ModelState.IsValid)
            {
                try
                {
                    trackRepo.UpdateTrack(track);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackExists(track.Id))
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
            ViewData["SupervisorID"] = new SelectList(trackRepo.GetAllInstructors(), "Id", "Email");
            ViewData["ProgramID"] = new SelectList(programRepo.GetAllPrograms(), "Id", "Name");
            return View(track);
        }

        // GET: Tracks/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          var track = trackRepo.GetTrackById(id.Value);
            if (track == null)
            {
                return NotFound();
            }
            return View(track);
        }

        // POST: Tracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var track = trackRepo.GetTrackById(id);

            if (track == null)
            {
                return NotFound();
            }
            trackRepo.DeleteTrack(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TrackExists(int id)
        {
            return trackRepo.GetTrackById(id) != null;
        }
    }
}
