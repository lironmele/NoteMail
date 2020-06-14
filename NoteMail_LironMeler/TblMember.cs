namespace NoteMail_LironMeler
{
    public class TblMember
    {
        public int MemberId { set; get; }
        public int[] c_ids { set; get; }
        public TblChat[] chats { set; get; }
        public bool Active;
        public TblMember(int MemberId)
        {
            this.MemberId = MemberId;
            c_ids = DBHandlerLiron.GetInstance().GetChatIdsWithMemberId(MemberId);
            chats = new TblChat[c_ids.Length];
            for (int i = 0; i < chats.Length; i++)
            {
                chats[i] = DBHandlerLiron.GetInstance().GetChat(c_ids[i], DBHandlerLiron.GetInstance().CountIds(c_ids[i]));
                chats[i].Att_ids = DBHandlerLiron.GetInstance().GetAttaIds(c_ids[i]);
                int count = 0;
                for (int j = 0; j < chats[i].Participants.Length; j++)
                    if (chats[i].Participants[j] != MemberId)
                        chats[i].Receiver[count++] = chats[i].Participants[j];
            }
            Active = true;
        }
        public void UpdateReceiver(int index)
        {
            int count = 0;
            for (int i = 0; i < chats[index].Participants.Length; i++)
                if (chats[index].Participants[i] != MemberId)
                    chats[index].Receiver[count++] = chats[index].Participants[i];
        }
    }
}
