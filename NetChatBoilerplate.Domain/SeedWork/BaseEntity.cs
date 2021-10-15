namespace NetChatBoilerplate.Domain.SeedWork
{
    using System;
    using System.Text.Json.Serialization;

    public record BaseEntity
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public BaseEntity()
        {
            this.CreatedAt = DateTimeOffset.UtcNow;
            this.UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
