using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Assistant_Bdd.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assistant_Interface.Models.ViewModels
{
    public class AssistantViewModel
    {
        public AssistantViewModel(Assistant dataAssistant)
        {
            IdAssistant = dataAssistant.IdAssistant;
            OpenAiAssisantId = dataAssistant.OpenAiAssisantId;
            NomAssistant = dataAssistant.NomAssistant;
            InstructionAssistant = dataAssistant.InstructionAssistant;
            DescriptionAssistant = dataAssistant.DescriptionAssistant;
            LogoAssistant = dataAssistant.LogoAssistant;
            AssistantActif = dataAssistant.AssistantActif;
            CreationAssistance = dataAssistant.CreationAssistance;
            UpdateAssistance = dataAssistant.UpdateAssistance;
            IdCreateurAssistant = dataAssistant.IdCreateurAssistant;
        }

        public AssistantViewModel()
        {
        }

        public int IdAssistant { get; set; }

        public string OpenAiAssisantId { get; set; }

        [MaxLength(100)]
        public string NomAssistant { get; set; }

        public string InstructionAssistant { get; set; }

        public string DescriptionAssistant { get; set; }

        [MaxLength(100)]
        public string LogoAssistant { get; set; }

        public bool AssistantActif { get; set; }

        public DateTime CreationAssistance { get; set; }

        public DateTime UpdateAssistance { get; set; }

        public string NomCreateurAssistant { get; set; }

        public string IdCreateurAssistant { get; set; }
    }
}
