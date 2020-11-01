using System.IO;
using Microsoft.EntityFrameworkCore;
using SharedProjects.Models;
using System.IO;

namespace SharedProjects
{
    public class ConfigDbContext : DbContext
    {
        protected  override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string _path = @"C:\Users\320087992\Documents\Bootcamp\case-study-II\alert-to-care-s21b9\AlertToCareApi\CaseStudy2Database.db";
            string pathCurrentDirectory = Directory.GetCurrentDirectory();
            string path = @"\CaseStudy2Database";
           string _path = pathCurrentDirectory + path;
            optionsBuilder.UseSqlite("Filename="+_path);

        }

        public DbSet<Icu> Icu { get; set; }
        public DbSet<Beds> Beds { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<VitalsLogs> VitalsLogs { get; set; }
    }

    
}
