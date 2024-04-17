namespace Attendance_Time_tracking_System.IRepos
{
    public interface IStudentRepo
    {
        public int AddStudent(Student student);
        public Student GetStudentById(int id);
        public void AddBulkStudents(List<Student> students);
    }
}
