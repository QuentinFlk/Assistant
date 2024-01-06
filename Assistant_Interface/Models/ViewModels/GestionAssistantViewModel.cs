using Assistant_Bdd.Models;
using System.ComponentModel.DataAnnotations;

namespace Assistant_Interface.Models.ViewModels
{
    public class GestionAssistantViewModel
    {
        public List<DetailAssistant> ListAssistantActif { get; set; }
        public List<DetailAssistant> ListAssistantInactif { get; set; }
    }

    public class DetailAssistant
    {
        public DetailAssistant(Assistant dataAssistant)
        {
            IdAssistant = dataAssistant.IdAssistant;
            NomAssistant = dataAssistant.NomAssistant;
            DescriptionAssistant = dataAssistant.DescriptionAssistant;
            AssistantActif = dataAssistant.AssistantActif;
            CreationAssistance = dataAssistant.CreationAssistance;
            IsAssistantGlobal = dataAssistant.IsAssistantGlobal;
        }

        public DetailAssistant()
        {
        }

        public int IdAssistant { get; set; }
        
        public string NomAssistant { get; set; }
        
        public string DescriptionAssistant { get; set; }
        
        public bool AssistantActif { get; set; }

        public DateTime CreationAssistance { get; set; }
        
        public string NomCreateurAssistant { get; set; }
        
        public bool IsAssistantGlobal { get; set; }
    }
}
