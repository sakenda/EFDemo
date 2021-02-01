using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sample1.DBModels.Configuration
{
    public class CourseConfiguration : EntityTypeConfiguration<Course>
    {
        public CourseConfiguration()
        {
            Property(c => c.CourseID).HasColumnName("KursNr");
            Property(c => c.Title).IsRequired();
            Property(c => c.Title).HasMaxLength(120);
            Property(c => c.CourseID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
        }
    }
}