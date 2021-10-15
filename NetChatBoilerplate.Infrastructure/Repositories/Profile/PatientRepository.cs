namespace NetChatBoilerplate.Infrastructure.Repositories.Profile
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NetChatBoilerplate.Domain.AggregatesModel.Profile;
    using NetChatBoilerplate.Domain.SeedWork;

    public class PatientRepository : IPatientRepository
    {
        private readonly CosmosContext _context;

        public IUnitOfWork UnitOfWork => this._context;

        public PatientRepository(CosmosContext context)
        {
            this._context = context;
        }

        public async Task<PatientEntity> FindByIdAsync(string id) => await this._context.Patients.FindAsync(id);

        public async Task<IReadOnlyList<PatientEntity>> FindAllAsync() => await this._context.Patients.ToListAsync();

        public PatientEntity Create(PatientEntity entity)
        {
            this._context.Add(entity);

            return entity;
        }

        public PatientEntity Update(PatientEntity entity)
        {
            this._context.Update(entity);

            return entity;
        }

        public void Delete(PatientEntity entity) => this._context.Remove(entity);
    }
}
