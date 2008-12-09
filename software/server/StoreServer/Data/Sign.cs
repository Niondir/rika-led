using System;
using System.Collections.Generic;
using System.Text;

namespace StoreServer.Data
{
    public class Sign
    {
        private int id;
        private string text;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
