namespace NetChatBoilerplate.Domain.AggregatesModel.Chat
{
    using NetChatBoilerplate.Domain.SeedWork;

    public record ParticipantEntity : BaseEntity
    {
        public string UserId { get; private set; }
        public string ProfileId { get; private set; }
        public string Name { get; private set; }
        public string ProfileUrl { get; private set; }
        public bool IsDoctor { get; private set; }

        public ParticipantEntity(string userId, string profileId, string name, string profileUrl, bool isDoctor = false) : base()
        {
            this.UserId = userId;
            this.ProfileId = profileId;
            this.Name = name;
            this.ProfileUrl = profileUrl;
            this.IsDoctor = isDoctor;
        }
    }
}
