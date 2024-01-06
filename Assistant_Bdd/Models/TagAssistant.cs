using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant_Bdd.Models
{
    public class TagAssistant
    {
        [Key, Required]
        public int IdTag { get; set; }
        [ForeignKey("IdTag")]
        public virtual Tag Tag{ get; set; }

        [Key, Required]
        public int IdAssistant { get; set; }
        [ForeignKey("IdAssistant")]
        public virtual Assistant Assistant { get; set; }
    }
}
