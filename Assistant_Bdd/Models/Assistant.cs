using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant_Bdd.Models
{
    public class Assistant
    {
        [Key, Required]
        public int IdAssistant { get; set; }

        [Required]
        public string OpenAiAssisantId { get; set; }

        [MaxLength(100)]
        public string NomAssistant { get; set; }

        [Column(TypeName = "ntext")]
        public string InstructionAssistant { get; set; }

        [Column(TypeName = "ntext")]
        public string DescriptionAssistant { get; set; }

        [MaxLength(100)]
        public string LogoAssistant { get; set; }

        public bool AssistantActif { get; set; }

        public DateTime CreationAssistance { get; set; }

        public DateTime UpdateAssistance { get; set; }

        public string IdCreateurAssistant { get; set; }
    }
}