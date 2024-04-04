using Attendance_Time_tracking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Time_tracking_System.Repos
{
    public class EmployeeRepo : IEmployeeRepo
    {
        readonly dbContext db;
        public EmployeeRepo(dbContext _db)
        {
            db = _db;
        }

        public IEnumerable<Employee> GetAll()
        {
            return  db.Employees.Where(a=>a.User_Status==true).ToList();
            
        }

        public Employee GetById(int id)
        {
            return db.Employees.SingleOrDefault(e => e.Id == id);
        }

        public void Add(Employee employee)
        {
            employee.User_Status = true;
            db.Employees.Add(employee);
            db.SaveChanges();
        }

        public void Delete(Employee employee)
        {
            //employee.Status = false;
            db.SaveChanges();
        }
            
        

        public void Update(Employee employee)
        {
            var model = db.Employees.FirstOrDefault(a => a.Id == employee.Id);
            model.F_name=employee.F_name;
            model.L_name=employee.L_name;
            model.Salary=employee.Salary;
            model.phone=employee.phone;
            model.Email=employee.Email;
            model.Password=employee.Password;
            model.phone = employee.phone;
           // model.roles= employee.roles;
        }


        public bool IsEmailExist(string email)=>
            db.Employees.Any(a=>a.Email==email);


    }
}
