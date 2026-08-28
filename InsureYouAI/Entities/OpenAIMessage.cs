namespace InsureYouAI.Entities
{
    public class OpenAIMessage
    {
        public int OpenAIMessageId { get; set; }
        public string MessageDetail { get; set; }
        public string ReceiveEmail { get; set; }
        public string ReceiveNameSurname { get; set; }
        public DateTime SendDate { get; set; }
    }
}
