namespace NoteMail_LironMeler
{
    public class TblMessage
    {
        public int m_id { get; set; }
        public int s_id { get; set; }
        public string content { get; set; }
        public string date { get; set; }
        public TblMessage(int m_id, int s_id, string content, string date)
        {
            this.m_id = m_id;
            this.s_id = s_id;
            this.content = content;
            this.date = date;
        }
    }
}
