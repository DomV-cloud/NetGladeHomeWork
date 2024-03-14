using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetGlade.Domain.Entities
{
    public class User
    {
        [Required]
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = null!;

        [Required]
        [JsonPropertyName("LastName")]
        public string LastName { get; set; } = null!;

        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;
    }
}
