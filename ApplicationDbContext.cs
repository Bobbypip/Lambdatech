using Lambdatech.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lambdatech
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Dads Seeeding
            var dad1 = new Dad() { Id = 1, Name = "Juan Robles" };
            var dad2 = new Dad() { Id = 2, Name = "Rodrigo Lopez" };
            modelBuilder.Entity<Dad>().HasData(new Dad[] { dad1, dad2 });

            // Moms Sedding
            Mom mom1 = new Mom() { Id = 1, Name = "Maria Stink" };
            Mom mom2 = new Mom() { Id = 2, Name = "Romina Tumbado" };
            modelBuilder.Entity<Mom>().HasData(new Mom[] { mom1, mom2 });

            // Student Sedding
            Student student1 = new Student() { Id = 1, Name = "Mario Robles Stink", DadId = 1, MomId = 1 };
            Student student2 = new Student() { Id = 2, Name = "Erick Lopez Tumbado", DadId = 2, MomId = 2 };
            Student student3 = new Student() { Id = 3, Name = "Martin Lopez Tumbado", DadId = 2, MomId = 2 };
            modelBuilder.Entity<Student>().HasData(new Student[] { student1, student2, student3 });

            // School Sedding
            School school1 = new School() { Id = 1, Name = "Instituto Kipling", StudentId = 1 };
            School school2 = new School() { Id = 2, Name = "Instituto Benavente", StudentId = 2 };
            School school3 = new School() { Id = 3, Name = "Instituto Capistrano", StudentId = 2 };
            School school4 = new School() { Id = 4, Name = "Instituto Capistrano", StudentId = 3 };
            modelBuilder.Entity<School>().HasData(new School[] { school1, school2, school3, school4 });
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Mom> Moms { get; set; }
        public DbSet<Dad> Dads { get; set; }
        public DbSet<School> Schools { get; set; }

    }
}
