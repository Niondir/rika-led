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
    public class ClientManager
    {
        private Dictionary<int, Client> clients;

        public ClientManager()
        {
            clients = new Dictionary<int, Client>();
        }


        public void AddClient(Client client)
        {
            clients[client.Session.Id] = client;
        }

        public void RemoveClient(Client client)
        {
            if (clients.ContainsKey(client.Session.Id)) {
                clients.Remove(client.Session.Id);
            }
        }
        
        

        public Client GetClient(SessionData session, IPEndPoint remoteEndPoint)
        {
            Client client;
            clients.TryGetValue(session.ID, out client);
            if (client != null)
            {
                if (!client.IP.Equals(remoteEndPoint.Address)) return null;

                client.CheckSession();
                
                if (!client.Authed)
                {
                    RemoveClient(client);
                }

                return client;
            }

            return null;
        }
    }
}
