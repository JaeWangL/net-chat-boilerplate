namespace NetChatBoilerplate.API.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using NetChatBoilerplate.Domain.AggregatesModel.Chat;

    public record ChatClient
    {
        public string ConnectionId { get; private set; }
        public ParticipantEntity? ParticipantInfo { get; private set; }

        public ChatClient(string connectionId)
        {
            this.ConnectionId = connectionId;
        }

        public ChatClient(string connectionId, ParticipantEntity info)
        {
            this.ConnectionId = connectionId;
            this.ParticipantInfo = info;
        }
    }

    public class ConsultationChatHub : Hub
    {
        private readonly IParticipantRepository _participantRepo;
        private readonly List<ChatClient> _chatClients = new();

        public ConsultationChatHub(IParticipantRepository participantRepo)
        {
            this._participantRepo = participantRepo ?? throw new ArgumentNullException(nameof(participantRepo));
        }

        public override Task OnConnectedAsync()
        {
            this._chatClients.Add(new ChatClient(this.Context.ConnectionId));

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var user = this._chatClients.Find((item) => item.ConnectionId == this.Context.ConnectionId);
            if (user is not null)
            {
                this._chatClients.Remove(user);
            }


            return base.OnDisconnectedAsync(exception);
        }
    }
}
