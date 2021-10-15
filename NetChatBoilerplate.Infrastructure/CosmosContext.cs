namespace NetChatBoilerplate.Infrastructure
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NetChatBoilerplate.Domain.AggregatesModel.Chat;
    using NetChatBoilerplate.Domain.AggregatesModel.Profile;
    using NetChatBoilerplate.Domain.SeedWork;
    using NetChatBoilerplate.Infrastructure.EntityConfigurations;

    // using NetChatBoilerplate.Infrastructure.EntityConfigurations;

    public class CosmosContext : DbContext, IUnitOfWork
    {
        // Chat
        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<ParticipantEntity> Participants { get; set; }
        public DbSet<RoomEntity> Rooms { get; set; }

        // Profile
        public DbSet<DoctorEntity> Doctors { get; set; }
        public DbSet<PatientEntity> Patients { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CosmosContext(DbContextOptions<CosmosContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
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
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
