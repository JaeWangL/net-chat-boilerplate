namespace NetChatBoilerplate.Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NetChatBoilerplate.Domain.AggregatesModel.Doctor;
    using NetChatBoilerplate.Domain.SeedWork;

    public class DoctorRepository : IDoctorRepository
    {
        private readonly CosmosContext context;

        public IUnitOfWork UnitOfWork => this.context;

        public DoctorRepository(CosmosContext context)
        {
            this.context = context;
        }

        public async Task<DoctorEntity> FindByIdAsync(string id)
        {
            var keyValues = new object[] { id };
            var family = await this.context.Doctors.FindAsync(keyValues);
            return family;
        }

        public async Task<IReadOnlyList<DoctorEntity>> FindAllAsync()
        {
            var families = await this.context.Doctors.ToListAsync();

            return families;
        }

        public async Task<DoctorEntity> CreateAsync(DoctorEntity entity)
        {
            await this.context.AddAsync(entity);

            return entity;
        }

        public async Task DeleteAsync(DoctorEntity entity)
        {
            this.context.Remove(entity);
        }

        public async Task UpdateAsync(DoctorEntity entity)
        {
            this.context.Update(entity);
        }
    }
}
