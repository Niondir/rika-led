using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.DataTypes;

namespace StoreServer.Data
{
    /// <summary>
    /// Contains a password for the server - client connection
    /// </summary>
    public class Password
    {
        private string value;

        /// <summary>
        /// Get the password as md5 hash
        /// </summary>
        public string MD5
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Set the password as plain text (will be stored as md5)
        /// You can't read the password in plain text again
        /// </summary>
        public string Plain
        {
            set { this.value = PasswordData.Encrypt(value); }
        }

        /// <summary>
        /// The daatobject, to receive the CommunicationAPI data type
        /// </summary>
        public PasswordData Data
        {
            get
            {
                PasswordData p = new PasswordData();
                p.Password = value;
                return p;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        public Password(PasswordData password)
        {
            this.value = password.Password;
        }

        /// <summary>
        /// Check if the bassword matches a plain password string
        /// </summary>
        /// <param name="plain">plain password string</param>
        /// <returns></returns>
        public bool Check(string plain)
        {
            return (PasswordData.Encrypt(plain) == value);
        }
    }
}
