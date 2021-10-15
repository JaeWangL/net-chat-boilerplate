namespace NetChatBoilerplate.API.Infrastructure.Options
{
    using System.ComponentModel.DataAnnotations;

    public class ApplicationOptions
    {
        public ApplicationOptions()
        {
            this.CacheProfiles = new CacheProfileOptions();
        }

        [Required]
        public CacheProfileOptions CacheProfiles { get; }

        [Required]
        public CompressionOptions Compression { get; set; } = default!;
    }
}
