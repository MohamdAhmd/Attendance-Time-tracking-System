using Attendance_Time_tracking_System.ViewModels;

namespace Attendance_Time_tracking_System.IRepos
{
    public interface IInstructorRepo
    {
        public Instructor GetInstructorById(int id);
        public List<InstructorViewModel> GetAllInstructors();
        public void AddInstructor(Instructor instructor);
        public void UpdateInstructor(Instructor instructor,int? id);
        public void DeleteInstructor(Instructor instructor);
        public List<ShowStudentsSupervisor> SuperVisorStudent(int supervisorid, DateOnly day, int SelectedTrack , string? DayStatus);

        public List<UserAbsence> ShowInstructorAbseneceDays(int id, string userstatus = "Absent");

    }
}
