using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using StoreServer.WebService;
using CommunicationAPI;
using CommunicationAPI.DataTypes;

namespace StoreServer.Data
{
    /// <summary>
    /// Benutzer und Rechteverwaltung
    /// </summary>
    public class UserManager
    {
        // TODO: Session IP gebunden? Mehrere Sessions pro IP?
        // Dann Dictonary<IPAdress, Session>
        private List<Session> sessions;

        public UserManager()
        {
            sessions = new List<Session>();
        }

        public Session Login(User user, IPEndPoint remoteEndPoint)
        {
            Session session = new Session(user);

            // TODO: user im Datamanager suchen
            if (session.User.Username == "gast" && session.User.Password.CheckPassword("gast")) 
            {
                // User already in session list?
                foreach (Session s in sessions)
                {
                    if (s.User.Username == user.Username)
                    {
                        // Refresh session
                        return s;
                    }
                }

                // TODO: Session timeout?
                // TODO: Generate valid uid
                session.Validate(100);
                //sessions.Add(session);
            }
            return session;
        }

        public bool Logout(IPEndPoint remoteEndPoint)
        {
            bool removed = false;
            foreach (Session s in sessions)
            {
               /* if (s.ID )
                {
                    sessions.Remove(s);
                    removed = true;
                    break;
                }
                * */
            }
            return removed;
        }

        public bool CheckAccess(Session session, AccessFlags flags)
        {
            if (!CheckSession(session)) return false;
            
            // TODO: Refresh session

            bool hasAccess = false;

            if (((AccessFlags)session.Access & flags) == flags)
            {
                hasAccess = true;
            }

            return hasAccess;
        }

        private bool CheckSession(Session session)
        {
            foreach (Session s in sessions)
            {
                if (s.ID == session.ID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
