using AlertToCareApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlertToCareApi
{
    public class ConfigDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Filename=D:\a\assist-purchase-s21b8\assist-purchase-s21b8\AlertToCareApi\CaseStudy2Database.db");
        }

        internal DbSet<Icu> Icu { get; set; }
        internal DbSet<Beds> Beds { get; set; }
        internal DbSet<Patients> Patients { get; set; }
        internal DbSet<VitalsLogs> VitalsLogs { get; set; }
    }
    
}
