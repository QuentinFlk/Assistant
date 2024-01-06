using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Assistant_Interface.Models.ViewModels
{
    public class AjoutAssistantViewModel
    {
        [MaxLength(100)]
        public string NomAssistant { get; set; }

        [Display(Name = "Instruction de l'assistant")]
        public string InstructionAssistant { get; set; }

        [Display(Name="Description de l'assistant")]
        public string DescriptionAssistant { get; set; }
        
        public string IdCreateurAssistant { get; set; }

        public List<SelectListItem> ListModelDisponible { get; set; }
        public void SetModelDisponible(List<OpenAI.Models.Model> listModelDisponible)
        {
            ListModelDisponible = listModelDisponible.Select(x => new SelectListItem
                { Text = x.Id, Value = x.Id }).ToList();
        }
        public string ChoixModel { get; set; }

        [Display(Name = "Activer le code interpréteur")]
        public bool IsCodeInterpreterEnable { get; set; }
    }
}
