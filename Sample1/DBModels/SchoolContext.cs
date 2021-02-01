using Sample1.DBModels.Configuration;
using System.Data.Entity;

namespace Sample1.DBModels
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base("conSchoolDB")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<SchoolContext>());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<SchoolContext>());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SchoolContext>());
        }

        // Mit DBSet Die Modelle dem DbContext hinzufügen
        public DbSet<Course> Courses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ScheduleConfiguration());
            modelBuilder.Configurations.Add(new CourseConfiguration());
            modelBuilder.Configurations.Add(new EnrollmentConfiguration());
            modelBuilder.Configurations.Add(new AddressConfiguration());
        }
    }
}