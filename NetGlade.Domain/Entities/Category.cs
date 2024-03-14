using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NetGlade.Domain.Entities
{
    public class Category
    {
        [Required]
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [JsonPropertyName("categoryName")]
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Item> Items { get; set; } = null!;
    }

}
