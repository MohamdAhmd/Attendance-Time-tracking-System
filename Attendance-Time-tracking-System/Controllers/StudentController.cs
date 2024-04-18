using Attendance_Time_tracking_System.Migrations;
using Attendance_Time_tracking_System.Models;
using Attendance_Time_tracking_System.Repos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;
using System.IO;
using Attendance_Time_tracking_System.IRepos;



namespace Attendance_Time_tracking_System.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly dbContext db;
        private readonly IStudentRepo studentRepo;

        public StudentController(dbContext _db, IStudentRepo _studentRepo)
        {
            db = _db;
            studentRepo = _studentRepo;
        }
        public IActionResult ShowAttendance()
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int id = int.Parse(userIdClaim.Value);

            var model = db.Attends.Where(a=>a.UserId==id )       
                        .Include(a => a.UserNavigation)
                        .Include(a => a.DaysNavigation)  
                        .ToList();
                        ;

            return View(model);       

        }

        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        public IActionResult Index()
        {
            return View(studentRepo.GetAllStudents());
        }
        [HttpGet]
        public IActionResult AddBulkStudents()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddBulkStudents(IFormFile excelFile)
        {
            int studentAdded = 0;

            try
            {
                if (excelFile == null || excelFile.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }
                else if (excelFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    return BadRequest("Only Excel files are allowed.");
                }

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var stream = new MemoryStream())
                {
                    excelFile.CopyTo(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value?.ToString()))
                            {
                                break;
                            }

                            // Extract values from Excel columns into variables
                            string email = worksheet.Cells[row, 1].Value?.ToString();
                            string password = worksheet.Cells[row, 2].Value?.ToString();
                            string fName = worksheet.Cells[row, 3].Value?.ToString();
                            string lName = worksheet.Cells[row, 4].Value?.ToString();
                            int? phone = Convert.ToInt32(worksheet.Cells[row, 5].Value);
                            bool userStatus = Convert.ToBoolean(worksheet.Cells[row, 6].Value);
                            string imagePath = worksheet.Cells[row, 7].Value?.ToString();
                            string faculty = worksheet.Cells[row, 8].Value?.ToString();
                            string university = worksheet.Cells[row, 9].Value?.ToString();
                            string specialization = worksheet.Cells[row, 10].Value?.ToString();
                            string st = worksheet.Cells[row, 11].Value?.ToString();
                            int _studentGraduationYear = int.Parse(st);
                            DateTime graduationYear = new DateTime(_studentGraduationYear, 1, 1);
                            // DateTime graduationYear = Convert.ToDateTime(worksheet.Cells[row, 10].Value);
                            int grade = Convert.ToInt32(worksheet.Cells[row, 12].Value);
                            string status = worksheet.Cells[row, 13].Value?.ToString();
                            int nextMinus = Convert.ToInt32(worksheet.Cells[row, 14].Value);
                            int trackId = Convert.ToInt32(worksheet.Cells[row, 15].Value);
                            int intakeId = Convert.ToInt32(worksheet.Cells[row, 16].Value);
                            string graduationDegree = worksheet.Cells[row, 17].Value?.ToString();

                            // Create Student object using extracted variables
                            Student s = new Student
                            {
                                Email = email,
                                Password = password,
                                F_name = fName,
                                L_name = lName,
                                phone = phone,
                                User_Status = userStatus,
                                image = imagePath,
                                Faculty = faculty,
                                University = university,
                                specialization = specialization,
                                GraduationYear = graduationYear,
                                Grade = grade,
                                status = status,
                                NextMinus = nextMinus,
                                TrackId = trackId,
                                IntakeID = intakeId,
                                GraduationDegree = graduationDegree
                            };
                            studentRepo.AddStudent(s,null);
                            studentAdded++;
                          




                        }
                        }
                    

                    return Ok($"Students uploaded successfully.\nNumber of Students Added: {studentAdded}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while processing the file. Please try again later.\nNumber of Students Added: {studentAdded}");
            }
        }


    }
}
