using System.IO;
using System.Drawing;
using System.Data.OleDb;

namespace NoteMail_LironMeler
{
    public class DBHandlerLiron
    {
        private OleDbConnection connection;

        static DBHandlerLiron instance = null;

        public static DBHandlerLiron GetInstance()
        {
            if (instance == null)
                instance = new DBHandlerLiron();
            return instance; 
        }
        
        private DBHandlerLiron()
        {
            connection = new OleDbConnection
            {
                ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DB\DB_Liron.accdb; Persist Security Info=True;Jet OLEDB:Database Password=moreluck"
            };
        }

        public void DeletePlayer(int m_id)
        {
            int[] c_ids = GetChatIdsWithMemberId(m_id);
            connection.Open();
            OleDbCommand command = new OleDbCommand("update Member set deleted = true where id = " + m_id, connection);
            command.ExecuteNonQuery();
            for (int i = 0; i < c_ids.Length; i++)
            {
                command = new OleDbCommand("update Chat set deleted = true where id = " + c_ids[i], connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public string GetChatName(int m_id, int c_id)
        {
            string name = "";
            connection.Open();
            OleDbCommand command = new OleDbCommand("select Member.m_name from Member inner join Member_Chat on Member.id = Member_Chat.m_id where member.id <> " + m_id + " and Member_Chat.c_id = " + c_id, connection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                name += reader.GetValue(0).ToString() + ", ";
            if (name != "")
                name = name.Substring(0, name.Length - 2);
            connection.Close();
            return name;
        }

        public void UpdateName(int id, string name)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("update member set member.m_name = '" + name + "' where member.id = " + id, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public string GetAttachmentName(int a_id)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select att_name from attachment where id = " + a_id
            };
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            string name = reader.GetValue(0).ToString();
            connection.Close();
            return name;
        }
        
        public int CountAttIds(int id)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = @"SELECT count(*)
                                FROM Attachment INNER JOIN Chat_Attachment ON Attachment.id = Chat_Attachment.a_id
                                WHERE Chat_Attachment.c_id = " + id
            };
            OleDbDataReader reader = command.ExecuteReader();
            if (!reader.Read())
                return 0;
            int count = int.Parse(reader.GetValue(0).ToString());
            connection.Close();
            return count;
        }

        public int[] GetAttaIds(int id)
        {
            int size = CountAttIds(id);
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = @"SELECT Attachment.id
                                FROM Attachment INNER JOIN Chat_Attachment ON Attachment.id = Chat_Attachment.a_id
                                WHERE Chat_Attachment.c_id = " + id
            };
            OleDbDataReader reader = command.ExecuteReader();
            int[] ids = new int[size];
            int i = 0;
            while (reader.Read())
                ids[i++] = int.Parse(reader.GetValue(0).ToString());
            connection.Close();
            return ids;
        }

        public void DownloadAttachment(int id)
        {
            string name = GetAttachmentName(id);
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select attachment from attachment where id = " + id
            };
            byte[] vs = (byte[])command.ExecuteScalar();
            File.WriteAllBytes("Downloads/" + name, vs);
            connection.Close();
        }

        public string SelectFileDetails(int id)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select att_name, s_id from attachment where id = " + id
            };
            OleDbDataReader reader = command.ExecuteReader();
            if (!reader.Read())
                return "";
            string name = reader.GetValue(0).ToString();
            int s_id = int.Parse(reader.GetValue(1).ToString());
            connection.Close();
            return GetNameWithId(s_id) + ": " + name;
        }

        public void InsertAttachment(byte[] att, string fileName, int c_id, int s_id)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "insert into attachment ([attachment], [att_name], s_id, a_date) values (@attachment, @att_name, " + s_id + ", now())"
            };
            command.Parameters.AddWithValue("@attachment", att);
            command.Parameters.AddWithValue("@att_name", fileName);
            command.ExecuteNonQuery();
            command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select top 1 id from  attachment order by id desc"
            };
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int a_id = int.Parse(reader.GetValue(0).ToString());
            command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "insert into chat_attachment (c_id, a_id) values (" + c_id + ", " + a_id + ")"
            };
            command.ExecuteNonQuery();
            connection.Close();
        }

        public string GetStatus(int id)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = @"SELECT top 1 Status.s_text
                                FROM Status INNER JOIN Member_Status ON Status.id = Member_Status.s_id
                                WHERE Member_Status.m_id = " + id + " ORDER BY Status.id DESC"
            };
            OleDbDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                connection.Close();
                return "";
            }
            string Status = reader.GetValue(0).ToString();
            connection.Close();
            return Status;
        }

        public void InsertStatus(int m_id, string status)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "insert into status (s_text) values ('" + status + "')"
            };
            command.ExecuteNonQuery();
            command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select top 1 id from status order by id desc"
            };
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int s_id = int.Parse(reader.GetValue(0).ToString());
            command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "insert into member_status (m_id, s_id) values (" + m_id + ", " + s_id + ")"
            };
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void InsertChat(int m_id, int[] s_id)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "insert into chat (c_date) values (now())"
            };
            command.ExecuteNonQuery();
            command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select top 1 id from chat order by id desc"
            };
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int c_id = int.Parse(reader.GetValue(0).ToString());
            command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "insert into member_chat (c_id, m_id) values (" + c_id + ", " + m_id + ")"
            };
            command.ExecuteNonQuery();
            for (int i = 0; i < s_id.Length; i++)
            {
                command = new OleDbCommand
                {
                    Connection = connection,
                    CommandText = "insert into member_chat (c_id, m_id) values (" + c_id + ", " + s_id[i] + ")"
                };
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public int[] GetAllMembers(int id)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select count(id) from member where deleted = false and id <> " + id
            };
            OleDbDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                connection.Close();
                return new int[0];
            }
            int count = int.Parse(reader.GetValue(0).ToString());
            command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select id from member where deleted = false and id <> " + id
            };
            reader = command.ExecuteReader();
            int i = 0;
            int[] ids = new int[count];
            while (reader.Read())
                ids[i++] = int.Parse(reader.GetValue(0).ToString());
            connection.Close();
            return ids;
        }

        public Bitmap GetDefaultProfilePicture()
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select image from profilePicture where id  = 1"
            };
            byte[] image = (byte[])command.ExecuteScalar();
            MemoryStream stream = new MemoryStream(image, 78, image.Length - 78);
            Bitmap bitmap = new Bitmap(stream);
            connection.Close();
            return bitmap;
        }

        public Bitmap GetProfilePathWithMember(int id)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = @"SELECT ProfilePicture.image
                                FROM ProfilePicture INNER JOIN Member_ProfilePicture ON ProfilePicture.id = Member_ProfilePicture.pp_id
                                WHERE Member_ProfilePicture.m_id = " + id + " ORDER BY ProfilePicture.id DESC"
            };
            byte[] image = (byte[])command.ExecuteScalar();
            if (image == null)
            {
                connection.Close();
                return GetDefaultProfilePicture();
            }
            MemoryStream stream = new MemoryStream();
            stream.Write(image, 0, image.Length);
            connection.Close();
            return new Bitmap(stream);
        }

        public void InsertProfilePicture(byte[] stream, int m_id)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "insert into ProfilePicture ([image]) values (@image)"
            };
            command.Parameters.AddWithValue("@image", stream);
            command.ExecuteNonQuery();
            command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select top 1 id from ProfilePicture order by id desc"
            };
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int pp_id = int.Parse(reader.GetValue(0).ToString());
            command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "insert into Member_ProfilePicture (m_id, pp_id) values (" + m_id + ", " + pp_id + ")"
            };
            command.ExecuteNonQuery();
            connection.Close();
        }

        public int InsertMember(string name, string pass)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "insert into member (m_name, pass) values ('" + name + "', " + pass + ")"
            };
            command.ExecuteNonQuery();
            command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select top 1 id from member order by id desc"
            };
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int id = int.Parse(reader.GetValue(0).ToString());
            connection.Close();
            return id;
        }

        public void InsertMessage(string content, int s_id, int c_id)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "insert into message (content, text_date, sender_id) values ('" + content + "', now(), " + s_id + ")"
            };
            command.ExecuteNonQuery();
            command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select top 1 id from message order by id desc"
            };
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int m_id = int.Parse(reader.GetValue(0).ToString());
            command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "insert into chat_message (c_id, m_id) values (" + c_id + ", " + m_id + ")"
            };
            command.ExecuteNonQuery();
            connection.Close();
        }

        public string GetNameWithId(int id)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select m_name from member where id = " + id
            };
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            string name = reader.GetValue(0).ToString();
            connection.Close();
            return name;
        }

        public int GetIdWithName(string Name)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select id from member where deleted = false and m_name = '" + Name + "'"
            };
            OleDbDataReader reader = command.ExecuteReader();
            if (!reader.Read())
                return 0;
            int id = int.Parse(reader.GetValue(0).ToString());
            connection.Close();
            return id;
        }

        public int[] GetChatIdsWithMemberId(int id)
        {
            int count = DBHandlerLiron.GetInstance().CountChatIds(id);
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = @"select Member_Chat.c_id
                                from Chat inner join (Member inner join Member_Chat on Member.id = Member_Chat.m_id) on Chat.id = Member_Chat.c_id
                                where Chat.deleted = false and Member_Chat.m_id = " + id
            };
            OleDbDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                connection.Close();
                return new int[0];
            }
            int[] c_ids = new int[count];
            for (int i = 0; i < c_ids.Length; ++i, reader.Read())
                c_ids[i] = int.Parse(reader.GetValue(0).ToString());
            connection.Close();
            return c_ids;
        }

        public int CountChatIds(int id)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = @"select Count([c_id])
                                from Chat inner join (Member inner join Member_Chat on Member.id = Member_Chat.m_id) on Chat.id = Member_Chat.c_id
                                where Chat.deleted = False and Member.id = " + id
            };
            OleDbDataReader reader = command.ExecuteReader();
            if(!reader.Read())
            {
                connection.Close();
                return 0;
            }
            int count = int.Parse(reader.GetValue(0).ToString());
            connection.Close();
            return count;
        }

        public int CountIds(int id)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select count(m_id) from chat_message where c_id = " + id
            };
            OleDbDataReader reader = command.ExecuteReader();
            if (!reader.Read())
                return 0;
            int count = int.Parse(reader.GetValue(0).ToString());
            connection.Close();
            return count;
        }

        public TblChat GetChat(int id, int length)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select count(m_id) from member_chat where c_id = " + id
            };
            OleDbDataReader reader = command.ExecuteReader();
            if (!reader.Read())
                return null;
            int[] members = new int[int.Parse(reader.GetValue(0).ToString())];
            command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select m_id from member_chat where c_id = " + id
            };
            reader = command.ExecuteReader();
            reader.Read();
            for (int i = 0; i < members.Length; i++, reader.Read())
                members[i] = int.Parse(reader.GetValue(0).ToString());
            TblChat Chat;
            if (length > 0)
            {
                command = new OleDbCommand
                {
                    Connection = connection,
                    CommandText = @"SELECT Chat.c_date, Chat_Message.m_id, Message.content, Message.text_date, Message.sender_id
                                FROM Message INNER JOIN(Chat INNER JOIN Chat_Message ON Chat.id = Chat_Message.c_id) ON Message.id = Chat_Message.m_id
                                where c_id = " + id
                };
                reader = command.ExecuteReader();
                reader.Read();
                TblMessage[] messages = new TblMessage[length];
                for (int i = 0; i < length - 1; i++, reader.Read())
                    messages[i] = new TblMessage(int.Parse(reader.GetValue(1).ToString()), int.Parse(reader.GetValue(4).ToString()), reader.GetValue(2).ToString(), reader.GetValue(3).ToString());
                messages[length - 1] = new TblMessage(int.Parse(reader.GetValue(1).ToString()), int.Parse(reader.GetValue(4).ToString()), reader.GetValue(2).ToString(), reader.GetValue(3).ToString());
                Chat = new TblChat(id, reader.GetValue(0).ToString(), members, messages);
            }
            else
            {
                command = new OleDbCommand
                {
                    Connection = connection,
                    CommandText = "select c_date from chat where id = " + id
                };
                reader = command.ExecuteReader();
                reader.Read();
                Chat = new TblChat(id, reader.GetValue(0).ToString(), members);
            }
            connection.Close();
            return Chat;
        }

        public string GetPass(string table, string Name)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "select pass from " + table + " where deleted = false and " + table[0] + "_name = '" + Name + "'"
            };
            OleDbDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                connection.Close();
                return null;
            }
            string pass = reader.GetValue(0).ToString();
            connection.Close();
            return pass;
        }
    }
}
