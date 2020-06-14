namespace NoteMail_LironMeler
{
    public class TblChat
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int[] Participants { get; set; }
        public int[] Receiver { get; set; }
        public int[] Att_ids { get; set; }
        public TblMessage[] Messages { get; set; }
        public TblChat(int Id, string Date, int[] Participants)
        {
            this.Id = Id;
            this.Date = Date;
            this.Participants = Participants;
            Receiver = new int[Participants.Length - 1];
            Messages = new TblMessage[0];
            Att_ids = new int[0];
        }
        public TblChat(int Id, string Date, int[] Participants, TblMessage[] Messages)
        {
            this.Id = Id;
            this.Date = Date;
            this.Participants = Participants;
            Receiver = new int[Participants.Length - 1];
            this.Messages = Messages;
            Att_ids = new int[0];
        }
    }
}
