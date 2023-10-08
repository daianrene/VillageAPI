using System.ComponentModel.DataAnnotations;

namespace VillageAPI.Models
{
    public class Village
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public int Population { get; set; }
        public int M2 { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
