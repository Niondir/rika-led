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

        /// <summary>
        /// 
        /// </summary>
        public ClientManager()
        {
            clients = new Dictionary<int, Client>();
        }

        /// <summary>
        /// Add a client to the directory of known clients
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Client client)
        {
            clients[client.Session.Id] = client;
        }

        /// <summary>
        /// Remove client from the known client directory
        /// </summary>
        /// <param name="client"></param>
        public void RemoveClient(Client client)
        {
            if (clients.ContainsKey(client.Session.Id)) {
                clients.Remove(client.Session.Id);
            }
        }
        
        
        /// <summary>
        /// Get a client by session and ip
        /// </summary>
        /// <param name="session"></param>
        /// <param name="remoteEndPoint"></param>
        /// <returns></returns>
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
