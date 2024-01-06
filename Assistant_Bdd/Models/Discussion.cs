using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant_Bdd.Models
{
    public class Discussion
    {
        [Key, Required]
        public int IdDiscussion { get; set; }

        [Required]
        public string OpenAiDiscussionId { get; set; }

        [MaxLength(100)]
        public string StatutDiscussion { get; set; }

        public DateTime CreationDiscussion { get; set; }

        [ForeignKey("IdAssistant")]
        public virtual Assistant Assistant { get; set; }
    }
}