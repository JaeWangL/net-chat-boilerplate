namespace NetChatBoilerplate.Infrastructure.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore.ValueGeneration;
    using NetChatBoilerplate.Domain.AggregatesModel.Patient;

    internal class PatientTypeConfiguration : IEntityTypeConfiguration<PatientEntity>
    {
        public void Configure(EntityTypeBuilder<PatientEntity> builder)
        {
            builder.ToContainer("Patients");

            builder.Property(f => f.Id)
                .HasConversion<string>()
                .HasValueGenerator<SequentialGuidValueGenerator>();

        }
    }
}
