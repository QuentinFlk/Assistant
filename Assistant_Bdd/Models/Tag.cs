using System.ComponentModel.DataAnnotations;

namespace Assistant_Bdd.Models
{
    public class Tag
    {
        [Key, Required]
        public int IdTag { get; set; }

        [MaxLength(100)]
        public string LibelleTag { get; set; }

        public DateTime CreationTag { get; set; }
    }
}
