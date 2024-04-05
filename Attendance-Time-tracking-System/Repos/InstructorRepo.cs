using Attendance_Time_tracking_System.Models;
using Attendance_Time_tracking_System.ViewModels;

namespace Attendance_Time_tracking_System.Repos
{
    public class InstructorRepo : IInstructorRepo
    {
        readonly dbContext db;
        public InstructorRepo(dbContext _db)
        {
            db = _db;
        }
        public List<InstructorViewModel> GetAllInstructors()
        {
            var Instructors = (from user in db.Users
                                              join instructor in db.Instructors on user.Id equals instructor.Id
                                                             select new InstructorViewModel
                                                             {
                                                                 Id = instructor.Id,                                                                 
                                                                 Name=$"{user.F_name} {user.L_name}",
                                                                 Email = user.Email, 
                                                                 Password = user.Password,
                                                                 Salary = instructor.Salary,
                                                                 HireDate = instructor.HireDate, 
                                                                

                                                             }).ToList();
                                                             
                   

            return Instructors;
        }
        public Instructor GetInstructorById(int id)
        {
            return db.Instructors.FirstOrDefault(i => i.Id == id);
        }
       
        public void AddInstructor(Instructor instructor)
        {
            instructor.roles.Add(new Roles {  RoleId= 3 });
            db.Instructors.Add(instructor);
            db.SaveChanges();
        }
        public void UpdateInstructor(Instructor instructor)
        {
            
            db.Instructors.Update(instructor);
            db.SaveChanges();
        }
        public void DeleteInstructor(Instructor instructor)
        {
            db.Instructors.Remove(instructor);
            db.SaveChanges();
        }

    }
}
