using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.DataTypes;

namespace StoreServer.Data
{
    /// <summary>
    /// One session of a signle client
    /// </summary>
    public class Session
    {
        private static int lastID = 0;
        private static TimeSpan timeout = new TimeSpan(0, 1, 0);

        private int id;
        private int timestamp;

        /// <summary>
        /// Session id
        /// </summary>
        public int Id
        {
            get { return id; }
        }

        /// <summary>
        /// Timestamp of last activity
        /// </summary>
        public int Timestamp
        {
            get { return timestamp; }
        }

        /// <summary>
        /// The daatobject, to receive the CommunicationAPI data type
        /// </summary>
        public SessionData Data
        {
            get 
            { 
                SessionData s = new SessionData(id);
                s.Timestamp = timestamp;
                return s;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public Session(SessionData session)
        {
            this.id = session.ID;
            this.timestamp = session.Timestamp;
        }

        /// <summary>
        /// Get a session with a new uid
        /// </summary>
        /// <returns></returns>
        public static Session NewSession()
        {
            return new Session(new SessionData(++lastID));
        }

        /// <summary>
        /// Refresh the session timestamp
        /// </summary>
        public void Refresh()
        {
            this.timestamp = SessionData.Date2Timestamp(DateTime.Now);
        }

        /// <summary>
        /// Session still alive?
        /// </summary>
        public bool Alive
        {
            get
            {
                return (timestamp < SessionData.Date2Timestamp(DateTime.Now + timeout));
            }
        }
    }
}
