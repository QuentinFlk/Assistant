using System.ComponentModel.DataAnnotations;

namespace Assistant_Bdd.Models
{
    public class Document
    {
        [Key, Required]
        public int IdDocument { get; set; }

        [Required]
        public string OpenAiDocumentId { get; set; }

        [MaxLength(100)]
        public string NomDocument { get; set; }

        [MaxLength(20)]
        public string TypeDocument { get; set; }

        [MaxLength(100)]
        public string ReferentDocument { get; set; }

        public DateTime UpladDocument { get; set; }
    }
}