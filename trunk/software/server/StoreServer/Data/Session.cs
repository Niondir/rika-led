using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.DataTypes;

namespace StoreServer.Data
{
    public class Session
    {
        private static int lastID = 0;
        private static TimeSpan timeout = new TimeSpan(0, 1, 0);

        private int id;
        private int timestamp;

        public int Id
        {
            get { return id; }
        }

        public int Timestamp
        {
            get { return timestamp; }
        }

        public SessionData Data
        {
            get 
            { 
                SessionData s = new SessionData(id);
                s.Timestamp = timestamp;
                return s;
            }
        }

        public Session(SessionData session)
        {
            this.id = session.ID;
            this.timestamp = session.Timestamp;
        }

        public static Session NewSession()
        {
            return new Session(new SessionData(++lastID));
        }

        public void Refresh()
        {
            this.timestamp = SessionData.Date2Timestamp(DateTime.Now);
        }

        public bool Alive
        {
            get
            {
                return (timestamp < SessionData.Date2Timestamp(DateTime.Now + timeout));
            }
        }
    }
}
