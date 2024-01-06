namespace Assistant_Interface.Models.Session
{
    public class EchangeSessionViewModel
    {
        public string LastEchangeClientAssistant { get; set; }
        public List<string> ListEchangeClientAssistant { get; set; }
        public string OpenAiAssistantId { get; set; }
        public string OpenaiDiscussionId { get; set; }
    }
}
