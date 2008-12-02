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
        private Dictionary<int, Client> clients;

        public UserManager()
        {
            ipTable = new Dictionary<long, List<Client>>();
        }


        public void AddClient(Client client)
        {
            clients[client.Session.ID] = client;
        }

        public void RemoveClient(Client client)
        {
            if (clients.ContainsKey(client.Session.ID)) {
                clients.Remove(client.Session.ID);
            }
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
            Client client;
            clients.TryGetValue(session.ID, out client);
            if (client != null)
            {
                if (!client.IP.Equals(remoteEndPoint.Address)) return null;

                client.CheckSession();
                return client;
            }

            return null;
        }
    }
}
