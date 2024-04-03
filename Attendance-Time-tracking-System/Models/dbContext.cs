using Microsoft.EntityFrameworkCore;

namespace Attendance_Time_tracking_System.Models

{
    public class dbContext : DbContext
    {
        public dbContext()
        {
        }

        public dbContext(DbContextOptions<dbContext> options): base(options) { }
        
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Attend> Attends { get; set; }
        public virtual DbSet<Days> Days { get; set; }
        public virtual DbSet<Intake> Intakes { get; set; }
        public virtual DbSet<IntakeProgram> IntakesProgram { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<TrackDays> TrackDays { get; set; }
        public virtual DbSet<WorksIn> worksIns { get; set; }
        public virtual DbSet<RoleId> RoleIds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.UseTptMappingStrategy();
            });

            modelBuilder.Entity<Student>(entity => 
            {
                entity.Property(e => e.Grade).HasDefaultValue(250);
            });
            modelBuilder.Entity<Instructor>(entity =>
            {

                entity.HasMany(e => e.works)
                .WithOne(e => e.InstructorNavigation)
                .HasForeignKey(e => e.InstructorId)
                .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.HasOne(e=>e.InstructorNavigation).WithMany().HasForeignKey(e=>e.SupervisorID)
                .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<Attend>(entity =>
            {
                entity.HasKey(e => new {e.UserId , e.DayId });
            });

            modelBuilder.Entity<Days>(entity =>
            {
                entity.Property(e=>e.Status).HasDefaultValue("offline");
            });

            modelBuilder.Entity<Intake>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<IntakeProgram>(entity =>
            {
                entity.HasKey(e => new { e.IntakeId , e.ProgramId}); 
            });

            modelBuilder.Entity<RoleId>(entity =>
            {
                entity.HasIndex(e=>e.Name).IsUnique();
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });


            modelBuilder.Entity<TrackDays>(entity =>
            {
                entity.HasKey(e => new { e.DayId, e.TrackId });
            });

            modelBuilder.Entity<WorksIn>(entity =>
            {
                entity.HasKey(e => new { e.InstructorId, e.IntakeId, e.TrackId });
            });





            base.OnModelCreating(modelBuilder);
        }
    }
}
