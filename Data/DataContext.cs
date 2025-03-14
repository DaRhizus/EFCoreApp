using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Data
{
    public class DataContext:DbContext
    {   
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {}

        
        public DbSet<Student> _students { get; set; }
        public DbSet<Course> _courses { get; set; }
        public DbSet<CourseApply> _courseapplies { get; set; }
        public DbSet<Educator> _educators { get; set; }


    }
}