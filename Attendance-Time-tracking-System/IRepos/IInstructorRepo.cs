using Attendance_Time_tracking_System.ViewModels;

namespace Attendance_Time_tracking_System.IRepos
{
    public interface IInstructorRepo
    {
        public Instructor GetInstructorById(int id);
        public List<InstructorViewModel> GetAllInstructors();
        public void AddInstructor(Instructor instructor);
        public void UpdateInstructor(Instructor instructor);
        public void DeleteInstructor(Instructor instructor);
    }
}
