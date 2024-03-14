using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetGlade.Domain.Entities
{
    public class EANCode
    {
        [Key]
        [Required]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Required]
        [JsonPropertyName("code")]
        public string Code { get; set; } = null!;
    }
}
