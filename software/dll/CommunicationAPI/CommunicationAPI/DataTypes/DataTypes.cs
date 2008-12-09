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
        private int timestamp;
        
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public SessionData(int id)
        {
            this.id = id;
            this.timestamp = SessionData.Date2Timestamp(DateTime.Now);
        }

        public static int Date2Timestamp(DateTime date)
        {
            DateTime dateRef = new DateTime(1970, 1, 1);
            TimeSpan ts = new TimeSpan(date.Ticks - dateRef.Ticks);
            return (Convert.ToInt32(ts.TotalSeconds));
        }

    }

    /// <summary>
    /// Stores a password in MD5 format and offers encryption algorythms
    /// </summary>
    public struct PasswordData
    {
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
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
        /// <returns>Returns true if the password is matching</returns>
        public bool CheckPassword(string password)
        {
            string pw = PasswordData.Encrypt(password);
            bool res = this.password == pw;
            return res;
        }

        public bool CheckPassword(PasswordData password)
        {
            return password.Password == this.Password;
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

        public UserData(string username)
        {
            this.username = username;
            this.password = new PasswordData(String.Empty);
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

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public RegionData(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
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
