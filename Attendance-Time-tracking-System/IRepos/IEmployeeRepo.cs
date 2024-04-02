namespace Attendance_Time_tracking_System.IRepos
{
    public interface IEmployeeRepo
    {
        public IEnumerable<Employee> GetAll();

        public Employee GetById(int id);

        public void Update(Employee employee);

        public void Delete(Employee employee);

        public void Add(Employee employee);

        public bool IsEmailExist(string email);


    }
}
