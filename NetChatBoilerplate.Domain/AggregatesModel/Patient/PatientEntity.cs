namespace NetChatBoilerplate.Domain.AggregatesModel.Patient
{
    using System;
    using NetChatBoilerplate.Domain.SeedWork;

    public record PatientEntity : BaseEntity
    {
        public string Name { get; private set; }
        public string ProfileUrl { get; private set; }

        public PatientEntity(string name, string profileUrl)
        {
            this.Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
            this.ProfileUrl = !string.IsNullOrWhiteSpace(profileUrl) ? profileUrl : throw new ArgumentNullException(nameof(profileUrl));
        }
    }
}

