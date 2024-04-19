using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Attendance_Time_tracking_System.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Time_tracking_System.Controllers
{
    [Authorize(Roles = "Student-affairs")]
    public class StudentAffairsController : Controller
    {
        readonly dbContext db;
        readonly IUserRepo userRepo;
        readonly IStudentRepo studentRepo;
        public StudentAffairsController(dbContext _db, IUserRepo _userRepo , IStudentRepo _studentRepo)
        {
            db = _db;
            userRepo = _userRepo;
            studentRepo = _studentRepo;
            this.studentRepo.PutAllStudentsInAttendanceTable("online");
        }
        public IActionResult Index()
        {
            return View(studentRepo.GetAllStudents());
        }


        public IActionResult GetUsers(int value)
        {
            try
            {
                if (value == 2)
                {
                    var users = studentRepo.GetAllUsersWithRole(value, "online");
                    return Json(users);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        public IActionResult changeAttendstatus(int userId, bool value, int usertype)
        {
            try
            {
                if (usertype == 2)
                {
                    bool change = studentRepo.changeattendance(userId, value);
                    return change ? Ok(new { success = true }) : StatusCode(500, "Internal server error");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult changeLeavingstatus(int userId, bool value, int usertype)
        {
            try
            {
                bool change = userRepo.ChangeLeaveAttendance(userId, value);
                return change ? Ok(new { success = true }) : StatusCode(500, "Internal server error.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        public IActionResult TakeAllLate(int[] value, int userType)
        {
            if (value.Length > 0)
            {
                return studentRepo.ChangeAllStudentToLate(value) ? Ok(new { success = true }) : StatusCode(500, "Internal server error.");
            }
            return StatusCode(500, "No items to save");
        }


        public IActionResult showlayout()
        {
            return View("_Layout");
        }
        /// <summary>
        /// profile page
        /// </summary>
        /// <returns></returns>

        public IActionResult ProfilePage()
        {
            var instid = instructorid();
            var user = userRepo.GetUserEditById(instid);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(UserEditProfile user, IFormFile personalimages)
        {
            if (ModelState.IsValid)
            {
                if (await userRepo.EditUserInfo(user, personalimages) == true)
                {
                    return RedirectToAction("ProfilePage");
                }
                return RedirectToAction("ProfilePage");
            }
            else
            {
                return RedirectToAction("ProfilePage");
            }
        }

        public IActionResult ChangePassword()
        {
            var instid = instructorid();
            var userpass = userRepo.UserPassword(instid);
            return View(userpass);
        }
        [HttpPost]
        public IActionResult ChangePassword(UserPassword model)
        {
            if (ModelState.IsValid)
            {
                if (userRepo.updatePass(model))
                {
                    return RedirectToAction("ProfilePage");
                }
            }
            return View(model);
        }


        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        public int instructorid()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim != null)
            {
                string userId = userIdClaim.Value;
                return int.Parse(userId);
            }
            return 7;
        }



        public IActionResult StudentRequests()
        {
            var data = studentRepo.GetAllPendingStudents();
            return View(data);
        }
        public IActionResult ChangeStatus(int stdId, string status,string dummy)
        {
            studentRepo.ChangeStatus(stdId, status);
            return RedirectToAction("StudentRequests");
        }



        [HttpGet]
        [Authorize(Roles = "Student-affairs")]
        public IActionResult AddBulkStudents()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Student-affairs")]
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
                            studentRepo.AddStudent(s, null);
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


        [HttpGet]
        [Authorize(Roles = "Student-affairs")]
        public IActionResult Edit(int id)
        {
            var student = db.Students.Include(s => s.TrackNavigation).FirstOrDefault(s => s.Id == id);
            var tracks = db.Tracks.ToList();
            ViewBag.Tracks = tracks;
            return View(student);
        }
        [HttpPost]
        [Authorize(Roles = "Student-affairs")]
        public IActionResult Edit(Student s, int? id)
        {
            studentRepo.UpdateStudent(s, id);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Student-affairs")]
        public IActionResult Delete(Student student)
        {
            studentRepo.DeleteStudent(student);
            return RedirectToAction("Index");
        }


    }
}
