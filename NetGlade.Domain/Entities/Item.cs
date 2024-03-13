using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetGlade.Domain.Entities
{
    public class Item
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string ItemName { get; set; } = null!;

        [Required]
        public EANCode ItemEanCode { get; set; } = null!;

        [Required]
        public Category ItemCategory { get; set; } = null!;

        [Required]
        public Section ItemSection { get; set; } = null!;

    }
}
