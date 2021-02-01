using System.Data.Entity.ModelConfiguration;

namespace Sample1.DBModels.Configuration
{
    public class EnrollmentConfiguration : EntityTypeConfiguration<Enrollment>
    {
        public EnrollmentConfiguration()
        {
            HasKey(e => new { e.StudentID, e.ScheduleID });
        }
    }
}