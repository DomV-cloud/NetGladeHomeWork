using System.ComponentModel.DataAnnotations;

namespace NetGlade.Domain.Entities
{
    public class Section
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string SectionName { get; set; } = null!;
    }
}
