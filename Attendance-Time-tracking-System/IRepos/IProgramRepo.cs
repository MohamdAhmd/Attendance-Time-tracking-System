namespace Attendance_Time_tracking_System.IRepos
{
    public interface IProgramRepo
    {  
            public List<Models.Program> GetAllPrograms();
            public Models.Program GetProgramById(int id);
            public void AddProgram(Models.Program program);
            public void UpdateProgram(Models.Program program);
            public void DeleteProgram(int id);
        
    }
}
