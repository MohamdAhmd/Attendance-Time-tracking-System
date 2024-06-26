﻿namespace Attendance_Time_tracking_System.IRepos
{
    public interface IStudentRepo
    {
        public Task<int> AddStudent(Student student, IFormFile? personalimage);
        
        public Student GetStudentById(int id);
        public bool changeattendance(int userId, bool value);
        public List<AttendanceList> GetAllUsersWithRole(int? value, string daystatus);
        public bool PutAllStudentsInAttendanceTable(string daystatus);
        public bool ChangeAllStudentToLate(int[] ids);

        public List<Student> GetAllPendingStudents();

        public void ChangeStatus(int stdId, string status);


        public void AddBulkStudents(List<Student> students);
        public List<Student> GetAllStudents();
        public void UpdateStudent(Student student, int? id);
        public void DeleteStudent(Student student);
    }
}
