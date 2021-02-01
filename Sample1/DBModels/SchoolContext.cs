using System.Data.Entity;

namespace Sample1.DBModels
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base("conSchoolDB")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<SchoolContext>());
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}