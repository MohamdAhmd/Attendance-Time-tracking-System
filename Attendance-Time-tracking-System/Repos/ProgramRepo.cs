﻿
namespace Attendance_Time_tracking_System.Repos
{
    public class ProgramRepo : IProgramRepo
    {
        readonly dbContext db;
        public ProgramRepo(dbContext db)
        {
            this.db = db;
        }

        public void AddProgram(Models.Program program)
        {
            db.Programs.Add(program);
            db.SaveChanges();   
        }

        public void DeleteProgram(int id)
        {
            var program = db.Programs.FirstOrDefault(p => p.Id == id);
            db.Programs.Remove(program);
            db.SaveChanges();
        }

        public List<Models.Program> GetAllPrograms()
        {
            return db.Programs.ToList();
        }

        public Models.Program GetProgramById(int id)
        {
            return db.Programs.Find(id);
        }

        public void UpdateProgram(Models.Program program)
        {
            db.Programs.Update(program);
            db.SaveChanges();
        }
    }
}
