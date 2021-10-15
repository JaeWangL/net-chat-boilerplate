namespace NetChatBoilerplate.Infrastructure.Repositories.Chat
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NetChatBoilerplate.Domain.AggregatesModel.Chat;
    using NetChatBoilerplate.Domain.SeedWork;

    public class ParticipantRepository : IParticipantRepository
    {
        private readonly CosmosContext _context;

        public IUnitOfWork UnitOfWork => this._context;

        public ParticipantRepository(CosmosContext context)
        {
            this._context = context;
        }

        public async Task<ParticipantEntity> FindByIdAsync(string id) => await this._context.Participants.FindAsync(id);

        public async Task<IReadOnlyList<ParticipantEntity>> FindAllAsync() => await this._context.Participants.ToListAsync();

        public ParticipantEntity Create(ParticipantEntity entity)
        {
            this._context.Add(entity);

            return entity;
        }

        public ParticipantEntity Update(ParticipantEntity entity)
        {
            this._context.Update(entity);

            return entity;
        }

        public void Delete(ParticipantEntity entity) => this._context.Remove(entity);
    }
}
