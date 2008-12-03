using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using CommunicationAPI;

namespace CommunicationAPI.DataTypes
{
 
    public struct SessionData
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

        public SessionData(int id)
        {
            this.id = id;
            this.timestamp = DateTime.Now.Ticks;
        }

        public static SessionData NewSession
        {
            get 
            {
                return new SessionData(++lastID);
            }
        }
    }

    /// <summary>
    /// Stores a password in MD5 format and offers encryption algorythms
    /// </summary>
    public struct PasswordData
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

        public PasswordData(string password)
        {
            /// We can use the Encrypt method only if it is static. Because we can't access "this" yet.
            this.password = PasswordData.Encrypt(password);
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
            string pw = PasswordData.Encrypt(password);
            bool res = this.password == pw;
            return res;
        }

        public bool CheckPassword(PasswordData password)
        {
            return password.PasswordMD5 == this.PasswordMD5;
        }
    }

    public struct RoleData
    {
        private string name;
        private int flags;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Flags
        {
            get { return flags; }
            set { flags = value; }
        }

        public RoleData(string name)
        {
            this.name = name;
            this.flags = 0;
        }

        public void SetFlags(AccessFlags flags)
        {
            this.flags = (int)flags;
        }

        public void AddFlags(AccessFlags flags) 
        {
            this.flags |= (int)flags;
        }

        public void DeleteFlags(AccessFlags flags)
        {
            this.flags &= ~(int)flags;
        }

        public bool HasFlags(AccessFlags flags)
        {
            return (this.flags & (int)flags) == (int)flags; 
        }
    }

    public struct UserData
    {
        private string username;
        private PasswordData password;
        private RoleData role;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public PasswordData Password
        {
            get { return password; }
            set { password = value; }
        }

        public RoleData Role
        {
            get { return role; }
            set { role = value; }
        }

        public UserData(string username, string password)
        {
            this.username = username;
            this.password = new PasswordData(password);
            this.role = new RoleData("none");
        }

    }

    public struct LampData
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public LampData(int id)
        {
            this.id = id;
        }
    }

    public struct RegionData
    {
        private int id;
        private string name;
    }

    public struct ProductData
    {
        private SignData sign;
    }

    public struct SignData
    {
        private int id;
        private RegionData region;
        private string text;
        private int signType;
    }

    public struct TraceData
    {
    }

    public struct AdvertisementData
    {
        private RegionData region;
    }
}
