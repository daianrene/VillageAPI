
using System.ComponentModel.DataAnnotations;

namespace VillageAPI.Models.Dto
{
    public class VillageDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Population { get; set; }
        public int M2 { get; set; }
    }
}
