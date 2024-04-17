using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Attendance_Time_tracking_System.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepo studentRepo;
        public StudentController(IStudentRepo _studentRepo)
        {
            studentRepo = _studentRepo;
        }
        public IActionResult Index()
        {
            return View();
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
                            string image = worksheet.Cells[row, 7].Value?.ToString();
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

                            // Add the student to the repository or database
                            studentRepo.AddStudent(s);

                            studentAdded++;
                        }
                    }
                }

                return Ok($"Students uploaded successfully.\nNumber of Students Added: {studentAdded}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while processing the file. Please try again later.\nNumber of Students Added: {studentAdded}");
            }
        }
    }
}
