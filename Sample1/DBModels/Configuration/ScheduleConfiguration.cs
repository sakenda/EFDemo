using System.Data.Entity.ModelConfiguration;

namespace Sample1.DBModels.Configuration
{
    public class ScheduleConfiguration : EntityTypeConfiguration<Schedule>
    {
        public ScheduleConfiguration()
        {
            ToTable("PlannedCourses", schemaName: "Admin");
            Property(cd => cd.Start).HasColumnName("StartDate");
        }
    }
}