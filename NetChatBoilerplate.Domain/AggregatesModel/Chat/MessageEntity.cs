namespace NetChatBoilerplate.Domain.AggregatesModel.Chat
{
    using NetChatBoilerplate.Domain.SeedWork;

    public record MessageEntity : BaseEntity
    {
        public string ProfileId { get; private set; }
        public string Text { get; private set; }
        public string AttachmentUrl { get; private set; }

        public MessageEntity(string profileId, string text, string attachmentUrl) : base()
        {
            this.ProfileId = profileId;
            this.Text = text;
            this.AttachmentUrl = attachmentUrl;
        }
    }
}
