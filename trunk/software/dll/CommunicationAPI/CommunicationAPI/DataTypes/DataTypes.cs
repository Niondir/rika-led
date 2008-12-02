using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CommunicationAPI.DataTypes
{
 
    public struct Session
    {
        private int id;
        private long timestamp;
        private static int lastID = 0;
        
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public long Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public Session(int id)
        {
            this.id = id;
            this.timestamp = DateTime.Now.Ticks;
        }

        public static Session NewSession
        {
            get 
            {
                return new Session(++lastID);
            }
        }
    }

    /// <summary>
    /// Stores a password in MD5 format and offers encryption algorythms
    /// </summary>
    public struct Password
    {
        private string password;

        public string PasswordMD5
        {
            get { return password; }
            set { password = value; }
        }

        public string PasswordClear
        {
            set { password = value; }
            get { return password; }
        }

        public Password(string password)
        {
            /// We can use the Encrypt method only if it is static. Because we can't access "this" yet.
            this.password = Password.Encrypt(password);
        }

        public static string Encrypt(string value)
        {
            MD5CryptoServiceProvider cryptMD5 = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(value);
            bs = cryptMD5.ComputeHash(bs);
            string res = BitConverter.ToString(bs);
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password">the password to check</param>
        /// <param name="isEncrypted">is the password already encrypted?</param>
        /// <returns>Returns true if the password is matching</returns>
        public bool CheckPassword(string password)
        {
            string pw = Password.Encrypt(password);
            bool res = this.password == pw;
            return res;
        }

        public bool CheckPassword(Password password)
        {
            return password.PasswordMD5 == this.PasswordMD5;
        }
    }

    public struct User
    {
        private string username;
        private Password password;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public Password Password
        {
            get { return password; }
            set { password = value; }
        }

        public User(string username, string password)
        {
            this.username = username;
            this.password = new Password(password);
        }

    }

    public struct Lamp
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public Lamp(int id)
        {
            this.id = id;
        }
    }

    public struct Region
    {
    }

    public struct Product
    {
    }

    public struct Sign
    {
    }

    public struct Trace
    {
    }

    public struct Advertisement
    {
    }
}
