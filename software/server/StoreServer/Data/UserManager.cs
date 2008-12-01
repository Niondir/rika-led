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
        private Dictionary<long, List<Client>> ipTable;
        private static int currentSessionID;

        public UserManager()
        {
            currentSessionID = 1;
            ipTable = new Dictionary<long, List<Client>>();
        }


        /// <summary>
        /// Validate the user
        /// Create an sessionID for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="remoteEndPoint"></param>
        /// <returns></returns>
        public Session Login(User user, IPEndPoint remoteEndPoint)
        {
            if (this.UserValid(user)) 
            {
                Session session = new Session(GenerateSessionID());
                Client client = new Client(session, remoteEndPoint);
                client.Authed = true;
                ipTable[remoteEndPoint.Address.Address].Add(client);

                return session;
            }

            throw new Exception("Invalid login");
        }

        private bool UserValid(User user)
        {
            // TODO: user im Datamanager suchen
            return (user.Username == "gast" && user.Password.CheckPassword("gast"));
        }

        private int GenerateSessionID()
        {
            return UserManager.currentSessionID++;
        }

        public bool Logout(Session session, IPEndPoint remoteEndPoint)
        {
            if (!ipTable.ContainsKey(remoteEndPoint.Address.Address)) return false;

            Client client = GetClient(session, remoteEndPoint);
            client.Authed = false;
            ipTable[remoteEndPoint.Address.Address].Remove(client);

            return true;
        }

        public bool CheckAccess(Session session, IPEndPoint remoteEndPoint, AccessFlags flags)
        {
            Client client = GetClient(session, remoteEndPoint);

            if (!client.Authed)
                return false;

            if ((client.AccessFlags & flags) == flags)
            {
                return true;
            }

            return false;
        }

        public Client GetClient(Session session, IPEndPoint remoteEndPoint)
        {
            if (!ipTable.ContainsKey(remoteEndPoint.Address.Address))
                return new Client(session, remoteEndPoint);

            List<Client> clients = ipTable[remoteEndPoint.Address.Address];

            foreach (Client c in clients)
            {
                if (c.Session.ID == session.ID)
                {
                    DateTime sDate = new DateTime(c.Session.Timestamp);
                    if (DateTime.Now - sDate > TimeSpan.FromMinutes(10))
                    {
                        Logout(session, remoteEndPoint);
                    }
                    else
                    {
                        c.RefreshSession();
                    }
                    
                    return c;
                }
            }

            return new Client(session, remoteEndPoint);
        }
    }
}
