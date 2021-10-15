namespace NetChatBoilerplate.Domain.AggregatesModel.Profile
{
    using NetChatBoilerplate.Domain.SeedWork;

    public record DoctorEntity : BaseEntity
    {
        public string UserId { get; private set; }
        public string Name { get; private set; }
        public string ProfileUrl { get; private set; }

        public DoctorEntity(string userId, string name, string profileUrl) : base()
        {
            this.UserId = userId;
            this.Name = name;
            this.ProfileUrl = profileUrl;
        }
    }
}
