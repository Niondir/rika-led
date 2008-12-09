using System;
using System.Collections.Generic;
using System.Text;

namespace StoreClient
{
    public class Connection
    {
        private static Connection instance;

        private void Init() {

        }

        public static Connection GetInstance() {
            if (instance == null) {
                instance = new Connection();
                instance.Init();
            }

            return instance;
        }

        public void Login(string user, string password)
        {

        }
    }
}
