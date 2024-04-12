﻿// <auto-generated />
using System;
using Attendance_Time_tracking_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Attendance_Time_tracking_System.Migrations
{
    [DbContext(typeof(dbContext))]
    [Migration("20240408200958_upate-attendance-studentdegree")]
    partial class upateattendancestudentdegree
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Attend", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("DayId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<bool>("StatusOut")
                        .HasColumnType("bit");

                    b.Property<int?>("StudentDegreeAtMoment")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("attendstatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "DayId");

                    b.HasIndex("DayId");

                    b.ToTable("Attends");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Days", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Intake", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("Id");

                    b.ToTable("Intakes");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.IntakeProgram", b =>
                {
                    b.Property<int>("IntakeId")
                        .HasColumnType("int");

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.HasKey("IntakeId", "ProgramId");

                    b.HasIndex("ProgramId");

                    b.ToTable("IntakesProgram");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Permission", b =>
                {
                    b.Property<DateTime>("day")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("PermissionBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PermissionStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PermissionType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("day", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Program", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("Id");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.RoleId", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("RoleIds");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Roles", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProgramID")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int>("SupervisorID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgramID");

                    b.HasIndex("SupervisorID");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.TrackDays", b =>
                {
                    b.Property<int>("DayId")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.Property<string>("Lecture1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lecture2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lecture3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartPeriod")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DayId", "TrackId");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackDays");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("F_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("L_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("User_Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("phone")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.WorksIn", b =>
                {
                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<int>("IntakeId")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("InstructorId", "IntakeId", "TrackId");

                    b.HasIndex("IntakeId");

                    b.HasIndex("TrackId");

                    b.ToTable("worksIns");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Employee", b =>
                {
                    b.HasBaseType("Attendance_Time_tracking_System.Models.User");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Instructor", b =>
                {
                    b.HasBaseType("Attendance_Time_tracking_System.Models.User");

                    b.Property<DateTime?>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<int?>("supervisorId")
                        .HasColumnType("int");

                    b.HasIndex("supervisorId");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Student", b =>
                {
                    b.HasBaseType("Attendance_Time_tracking_System.Models.User");

                    b.Property<int?>("AbsenceDays")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Faculty")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Grade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(250);

                    b.Property<string>("GraduationDegree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("GraduationYear")
                        .HasColumnType("datetime2");

                    b.Property<int>("IntakeID")
                        .HasColumnType("int");

                    b.Property<int>("NextMinus")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.Property<string>("University")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("specialization")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("IntakeID");

                    b.HasIndex("TrackId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Attend", b =>
                {
                    b.HasOne("Attendance_Time_tracking_System.Models.Days", "DaysNavigation")
                        .WithMany("attends")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Attendance_Time_tracking_System.Models.User", "UserNavigation")
                        .WithMany("attends")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DaysNavigation");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.IntakeProgram", b =>
                {
                    b.HasOne("Attendance_Time_tracking_System.Models.Intake", "IntakeNavigation")
                        .WithMany("intakePrograms")
                        .HasForeignKey("IntakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Attendance_Time_tracking_System.Models.Program", "ProgramNavigation")
                        .WithMany("intakePrograms")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IntakeNavigation");

                    b.Navigation("ProgramNavigation");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Permission", b =>
                {
                    b.HasOne("Attendance_Time_tracking_System.Models.Student", "StudentNavigation")
                        .WithMany("Permissions")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentNavigation");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Roles", b =>
                {
                    b.HasOne("Attendance_Time_tracking_System.Models.RoleId", "RoleNavigation")
                        .WithMany("Roles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Attendance_Time_tracking_System.Models.User", "UserNavigation")
                        .WithMany("roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoleNavigation");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Track", b =>
                {
                    b.HasOne("Attendance_Time_tracking_System.Models.Program", "ProgramNavigation")
                        .WithMany()
                        .HasForeignKey("ProgramID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Attendance_Time_tracking_System.Models.Instructor", "InstructorNavigation")
                        .WithMany()
                        .HasForeignKey("SupervisorID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("InstructorNavigation");

                    b.Navigation("ProgramNavigation");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.TrackDays", b =>
                {
                    b.HasOne("Attendance_Time_tracking_System.Models.Days", "DayNavigation")
                        .WithMany("trackDays")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Attendance_Time_tracking_System.Models.Track", "TrackNavigation")
                        .WithMany("trackDays")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DayNavigation");

                    b.Navigation("TrackNavigation");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.WorksIn", b =>
                {
                    b.HasOne("Attendance_Time_tracking_System.Models.Instructor", "InstructorNavigation")
                        .WithMany("works")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Attendance_Time_tracking_System.Models.Intake", "IntakeNavigation")
                        .WithMany("Works")
                        .HasForeignKey("IntakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Attendance_Time_tracking_System.Models.Track", "TrackNavigation")
                        .WithMany("Works")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InstructorNavigation");

                    b.Navigation("IntakeNavigation");

                    b.Navigation("TrackNavigation");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Employee", b =>
                {
                    b.HasOne("Attendance_Time_tracking_System.Models.User", null)
                        .WithOne()
                        .HasForeignKey("Attendance_Time_tracking_System.Models.Employee", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Instructor", b =>
                {
                    b.HasOne("Attendance_Time_tracking_System.Models.User", null)
                        .WithOne()
                        .HasForeignKey("Attendance_Time_tracking_System.Models.Instructor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Attendance_Time_tracking_System.Models.Track", "supervisor")
                        .WithMany()
                        .HasForeignKey("supervisorId");

                    b.Navigation("supervisor");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Student", b =>
                {
                    b.HasOne("Attendance_Time_tracking_System.Models.User", null)
                        .WithOne()
                        .HasForeignKey("Attendance_Time_tracking_System.Models.Student", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Attendance_Time_tracking_System.Models.Intake", "IntakeNavigation")
                        .WithMany("Students")
                        .HasForeignKey("IntakeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Attendance_Time_tracking_System.Models.Track", "TrackNavigation")
                        .WithMany("Students")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IntakeNavigation");

                    b.Navigation("TrackNavigation");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Days", b =>
                {
                    b.Navigation("attends");

                    b.Navigation("trackDays");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Intake", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("Works");

                    b.Navigation("intakePrograms");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Program", b =>
                {
                    b.Navigation("intakePrograms");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.RoleId", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Track", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("Works");

                    b.Navigation("trackDays");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.User", b =>
                {
                    b.Navigation("attends");

                    b.Navigation("roles");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Instructor", b =>
                {
                    b.Navigation("works");
                });

            modelBuilder.Entity("Attendance_Time_tracking_System.Models.Student", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
