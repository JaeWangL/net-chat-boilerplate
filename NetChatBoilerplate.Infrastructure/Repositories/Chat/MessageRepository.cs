namespace NetChatBoilerplate.Infrastructure.Repositories.Chat
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NetChatBoilerplate.Domain.AggregatesModel.Chat;
    using NetChatBoilerplate.Domain.SeedWork;

    public class MessageRepository : IMessageRepository
    {
        private readonly CosmosContext _context;

        public IUnitOfWork UnitOfWork => this._context;

        public MessageRepository(CosmosContext context)
        {
            this._context = context;
        }

        public async Task<MessageEntity> FindByIdAsync(string id) => await this._context.Messages.FindAsync(id);

        public async Task<IReadOnlyList<MessageEntity>> FindAllAsync() => await this._context.Messages.ToListAsync();

        public MessageEntity Create(MessageEntity entity)
        {
            this._context.Add(entity);

            return entity;
        }

        public MessageEntity Update(MessageEntity entity)
        {
            this._context.Update(entity);

            return entity;
        }

        public void Delete(MessageEntity entity) => this._context.Remove(entity);
    }
}
