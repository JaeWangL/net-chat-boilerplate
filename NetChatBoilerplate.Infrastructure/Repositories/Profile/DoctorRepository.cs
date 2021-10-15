namespace NetChatBoilerplate.Infrastructure.Repositories.Profile
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NetChatBoilerplate.Domain.AggregatesModel.Profile;
    using NetChatBoilerplate.Domain.SeedWork;

    public class DoctorRepository : IDoctorRepository
    {
        private readonly CosmosContext _context;

        public IUnitOfWork UnitOfWork => this._context;

        public DoctorRepository(CosmosContext context)
        {
            this._context = context;
        }

        public async Task<DoctorEntity> FindByIdAsync(string id) => await this._context.Doctors.FindAsync(id);

        public async Task<IReadOnlyList<DoctorEntity>> FindAllAsync() => await this._context.Doctors.ToListAsync();

        public DoctorEntity Create(DoctorEntity entity)
        {
            this._context.Add(entity);

            return entity;
        }

        public DoctorEntity Update(DoctorEntity entity)
        {
            this._context.Update(entity);

            return entity;
        }

        public void Delete(DoctorEntity entity) => this._context.Remove(entity);
    }
}
