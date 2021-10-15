namespace NetChatBoilerplate.Infrastructure.Repositories.Chat
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NetChatBoilerplate.Domain.AggregatesModel.Chat;
    using NetChatBoilerplate.Domain.SeedWork;

    public class RoomRepository : IRoomRepository
    {
        private readonly CosmosContext _context;

        public IUnitOfWork UnitOfWork => this._context;

        public RoomRepository(CosmosContext context)
        {
            this._context = context;
        }

        public async Task<RoomEntity> FindByIdAsync(string id) => await this._context.Rooms.FindAsync(id);

        public async Task<IReadOnlyList<RoomEntity>> FindAllAsync() => await this._context.Rooms.ToListAsync();

        public RoomEntity Create(RoomEntity entity)
        {
            this._context.Add(entity);

            return entity;
        }

        public RoomEntity Update(RoomEntity entity)
        {
            this._context.Update(entity);

            return entity;
        }

        public void Delete(RoomEntity entity) => this._context.Remove(entity);
    }
}

