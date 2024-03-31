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
   
    public class ProgramController : Controller
    {
        IProgramRepo programRepo;
        public ProgramController(IProgramRepo programRepo)
        {
            this.programRepo = programRepo;
        }
        // GET: Program
        public  IActionResult Index()
        {
            return View(programRepo.GetAllPrograms());
        }

        // GET: Program/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var program =  programRepo.GetProgramById(id.Value);
            if (program == null)
            {
                return NotFound();
            }
            return View(program);
        }

        // GET: Program/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Program/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Models.Program program)
        {
           if(!ModelState.IsValid)
            {
                return View("create",program);
            }
            programRepo.AddProgram(program);
            return RedirectToAction("index");
        }

        // GET: Program/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = programRepo.GetProgramById(id.Value);
            if (program == null)
            {
                return NotFound();
            }
            return View(program);
        }

        // POST: Program/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Models.Program program)
        {
            if (id != program.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    programRepo.UpdateProgram(program);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramExists(program.Id))
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
            return View(program);
        }

        // GET: Program/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = programRepo.GetProgramById(id.Value);
            if (program == null)
            {
                return NotFound();
            }

            return View(program);
        }

        // POST: Program/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var program = programRepo.GetProgramById(id);
            
            if(program == null)
            {
                return NotFound();
            }
            programRepo.DeleteProgram(id);
            return RedirectToAction(nameof(Index));

        }
        private bool ProgramExists(int id)
        {
            return programRepo.GetProgramById(id) != null;
        }
    }
}
