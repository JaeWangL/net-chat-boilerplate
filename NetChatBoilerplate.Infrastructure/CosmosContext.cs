namespace NetChatBoilerplate.Infrastructure
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NetChatBoilerplate.Domain.AggregatesModel.Doctor;
    using NetChatBoilerplate.Domain.AggregatesModel.Patient;
    using NetChatBoilerplate.Domain.SeedWork;
    using NetChatBoilerplate.Infrastructure.EntityConfigurations;

    public class CosmosContext : DbContext, IUnitOfWork
    {
        public DbSet<DoctorEntity> Doctors { get; set; }
        public DbSet<PatientEntity> Patients { get; set; }

        public CosmosContext(DbContextOptions<CosmosContext> options) : base(options) {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DoctorTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PatientTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
