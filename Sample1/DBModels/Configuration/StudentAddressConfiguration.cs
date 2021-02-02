using System.Data.Entity.ModelConfiguration;

namespace Sample1.DBModels.Configuration
{
    public class StudentAddressConfiguration : EntityTypeConfiguration<StudentAddress>
    {
        public StudentAddressConfiguration()
        {
            Property(p => p.City).IsRequired();
            Property(p => p.City).HasMaxLength(40);

            Property(p => p.Street).IsRequired();
            Property(p => p.Street).HasMaxLength(50);

            Property(p => p.Country).IsRequired();
            Property(p => p.Country).HasMaxLength(20);
        }
    }
}