﻿using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.Models
{
    public class Days
    {
        [Key]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }

        public List<Attend> attends { get; set; } = new List<Attend>();
        public List<TrackDays> trackDays { get; set; } = new List<TrackDays>();
    }
}
