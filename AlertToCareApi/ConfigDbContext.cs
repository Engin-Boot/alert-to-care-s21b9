using AlertToCareApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlertToCareApi
{
    public class ConfigDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string _path = @"D:\a\alert-to-care-s21b9\alert-to-care-s21b9\CaseStudy2Database.db";
            optionsBuilder.UseSqlite("Filename="+_path);
        }

        internal DbSet<Icu> Icu { get; set; }
        internal DbSet<Beds> Beds { get; set; }
        internal DbSet<Patients> Patients { get; set; }
        internal DbSet<VitalsLogs> VitalsLogs { get; set; }
    }
    
}
