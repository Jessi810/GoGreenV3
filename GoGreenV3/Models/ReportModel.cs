using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GoGreenV3.Models
{
    public class AlterReport
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Location { get; set; }
    }

    //public class BackupReport
    //{
    //    public int Id { get; set; }

    //    [DataType(DataType.DateTime)]
    //    public DateTime Date { get; set; }

    //    [StringLength(250, ErrorMessage = "Maximum of {0} characters only")]
    //    public string Address { get; set; }

    //    [StringLength(50, ErrorMessage = "Maximum of {0} characters only")]
    //    public string Status { get; set; }
    //}

    //public class EmergencyReport
    //{
    //    public int Id { get; set; }

    //    [DataType(DataType.DateTime)]
    //    public DateTime Date { get; set; }

    //    [StringLength(250, ErrorMessage = "Maximum of {0} characters only")]
    //    public string Address { get; set; }

    //    [StringLength(50, ErrorMessage = "Maximum of {0} characters only")]
    //    public string EmergencyType { get; set; }

    //    [StringLength(50, ErrorMessage = "Maximum of {0} characters only")]
    //    public string EmergencyStatus { get; set; }
    //}

    //public class ReinforcementReport
    //{
    //    public int Id { get; set; }

    //    [DataType(DataType.DateTime)]
    //    public DateTime Date { get; set; }

    //    [StringLength(250, ErrorMessage = "Maximum of {0} characters only")]
    //    public string Address { get; set; }

    //    public int ReinforcementCount { get; set; }

    //    public int VehicleCount { get; set; }
    //}

    //public class AllReport
    //{
    //    public int Id { get; set; }

    //    public AlterReport Alter { get; set; }

    //    public BackupReport Backup { get; set; }

    //    public EmergencyReport Emergency { get; set; }

    //    public ReinforcementReport Reinforcement { get; set; }
    //}

    public class ReportDbContext : DbContext
    {
        public ReportDbContext() : base("ReportsConnection") { }

        public DbSet<AlterReport> Alters { get; set; }
        //public DbSet<BackupReport> Backups { get; set; }
        //public DbSet<EmergencyReport> Emergencies { get; set; }
        //public DbSet<ReinforcementReport> Reinforcements { get; set; }
        //public DbSet<AllReport> AllReports { get; set; }
    }
}