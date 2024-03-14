using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NetGlade.Domain.Entities
{
    public class Item
    {
        [Key]
        [Required]
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [JsonPropertyName("itemName")]
        public string ItemName { get; set; } = null!;

        [Required]
        [JsonPropertyName("itemEanCode")]
        public EANCode ItemEanCode { get; set; } = null!;

        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        
        [ForeignKey("SectionId")]
        public Guid SectionId { get; set; }
    }

}
