namespace Attendance_Time_tracking_System.IRepos
{
    public interface IInstructorRepo
    {
        public Instructor GetInstructorById(int id);
        public Instructor GetInstructorByUserId(int id);
        public List<Instructor> GetAllInstructors();
        public void AddInstructor(Instructor instructor);
        public void UpdateInstructor(Instructor instructor);
        public void DeleteInstructor(Instructor instructor);
    }
}
