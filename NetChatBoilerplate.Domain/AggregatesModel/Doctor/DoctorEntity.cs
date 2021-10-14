namespace NetChatBoilerplate.Domain.AggregatesModel.Doctor
{
    using System;
    using NetChatBoilerplate.Domain.SeedWork;

    public record DoctorEntity : BaseEntity
    {
        public string Name { get; private set; }
        public string ProfileUrl { get; private set; }

        public DoctorEntity(string name, string profileUrl)
        {
            this.Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
            this.ProfileUrl = !string.IsNullOrWhiteSpace(profileUrl) ? profileUrl : throw new ArgumentNullException(nameof(profileUrl));
        }
    }
}
