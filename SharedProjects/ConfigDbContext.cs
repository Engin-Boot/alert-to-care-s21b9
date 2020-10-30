﻿using Microsoft.EntityFrameworkCore;
using SharedProjects.Models;

namespace SharedProjects
{
    public class ConfigDbContext : DbContext
    {
        protected  override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string _pathD=Directory.GetCurrentDirectory();
           // Directory.SetCurrentDirectory("CaseStudy2Database.db");
            //string _path = @"CaseStudy2Database.db";
           //string _path1 = _pathD + _path;
         // string _path = @"C:\Users\320087992\Documents\Bootcamp\case-study-II\alert-to-care-s21b9\AlertToCareApi\CaseStudy2Database.db";
            string _path =@"C:\Users\320087877\OneDrive - Philips\Documents\GitHub\alert-to-care-s21b9\AlertToCareApi\CaseStudy2Database.db";
           
            optionsBuilder.UseSqlite("Filename="+_path);

        }

        public DbSet<Icu> Icu { get; set; }
        public DbSet<Beds> Beds { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<VitalsLogs> VitalsLogs { get; set; }
    }

    
}
