using Assistant_Bdd.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant_Interface.Models.ViewModels
{
    public class AccueilViewModel
    {
        public string EchangeClientAssistant { get; set; }
        public List<string> ListEchangeClientAssistant { get; set; }

        public List<SelectListItem> ListAssistantDisponible { get; set; }
        public void SetAssistantDisponible(List<Assistant> listAssistantDisponible)
        {
            ListAssistantDisponible = listAssistantDisponible.Select(x => new SelectListItem
                {Text = x.NomAssistant, Value = x.IdAssistant.ToString()}).ToList();
        }
        public string ChoixAssistant { get; set; }
    }
}
