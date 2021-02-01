using System.Data.Entity.ModelConfiguration;

namespace Sample1.DBModels.Configuration
{
    public class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            Property(s => s.LastName).IsConcurrencyToken();
        }
    }
}