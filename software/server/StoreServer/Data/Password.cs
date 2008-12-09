using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.DataTypes;

namespace StoreServer.Data
{
    public class Password
    {
        private string value;

        public string MD5
        {
            get { return value; }
        }

        public string Plain
        {
            set { this.value = PasswordData.Encrypt(value); }
        }

        public PasswordData Data
        {
            get
            {
                PasswordData p = new PasswordData();
                p.Password = value;
                return p;
            }
        }

        public Password(PasswordData password)
        {
            this.value = password.Password;
        }

        public bool Check(string plain)
        {
            return (PasswordData.Encrypt(plain) == value);
        }
    }
}
