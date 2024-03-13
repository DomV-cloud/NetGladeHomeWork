using System.ComponentModel.DataAnnotations;

namespace NetGlade.Domain.Entities
{
    public class EANCode
    {
        [Key]
        [Required]
        public string Code { get; set; } = null!;

    }
}
