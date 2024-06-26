using Attendance_Time_tracking_System.Models;
using Attendance_Time_tracking_System.Repos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Time_tracking_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Account/login";
            });

            //////////////////////////////
            ////////connection////////////
            //////////////////////////////

            var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("No connection string was found") ;

            builder.Services.AddDbContext<dbContext>(optionBuilder =>
            {
                //"Data Source=tcp:ititeam.database.windows.net,1433;Initial Catalog=Attendance;User ID=sqladmin@pdsqlproject;Password=Admin12345;Connect Timeout=1200"
                optionBuilder.UseSqlServer(connectionstring);
            });
            builder.Services.AddScoped<IBlobRepo, BlobRepo>();
            builder.Services.AddScoped<IAttendRepo, AttendRepo>();
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IDaysRepo, DaysRepo>();
            builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            builder.Services.AddScoped<IInstructorRepo, InstructorRepo>();
            builder.Services.AddScoped<IIntakeRepo, IntakeRepo>();
            builder.Services.AddScoped<IIntakeProgramRepo, IntakeProgramRepo>();
            builder.Services.AddScoped<IProgramRepo, ProgramRepo>();
            builder.Services.AddScoped<IRoleRepo, RoleRepo>();
            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<ITrackRepo, TrackRepo>();
            builder.Services.AddScoped<ITrackDaysRepo, TrackDaysRepo>();
            builder.Services.AddScoped<IWorksInRepo, WorksInRepo>();
            builder.Services.AddScoped<IPermissionRepo, PermissionRepo>();

            //end of connections
            //////////////////////////////////////////////////
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
