namespace NetChatBoilerplate.Domain.SeedWork
{
    using System;
    using System.Text.Json.Serialization;

    public record BaseEntity
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
