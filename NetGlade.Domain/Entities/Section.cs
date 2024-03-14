using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NetGlade.Domain.Entities
{
    public class Section
    {
        [Key]
        [Required]
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [JsonPropertyName("sectionName")]
        public string SectionName { get; set; } = null!;
    }
}
