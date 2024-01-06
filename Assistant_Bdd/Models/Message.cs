using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant_Bdd.Models
{
    public class Message
    {
        [Key, Required]
        public int IdMessage { get; set; }

        [Required]
        public string OpenAiMessageId { get; set; }

        [MaxLength(100)]
        public string StatutMessage { get; set; }

        public DateTime CreationMessage { get; set; }

        [ForeignKey("IdDiscussion")]
        public virtual Discussion Discussion { get; set; }
    }
}