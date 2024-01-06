using System.ComponentModel.DataAnnotations;

namespace Assistant_Bdd.Models
{
    public class Client
    {
        [Key, Required]
        public int IdClient { get; set; }

        [MaxLength(100)]
        public string NomClient { get; set; }

        [MaxLength(150)]
        public string EmailClient { get; set; }

        [MaxLength(30)]
        public string TelephoneClient { get; set; }

        public bool ClientActif { get; set; }
    }
}