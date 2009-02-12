using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.DataTypes;

namespace StoreServer.Data
{
    /// <summary>
    /// The data for one signe in the supermarket
    /// </summary>
    public class Sign
    {
        private int id;
        private Region region;

        /// <summary>
        /// The sign id
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// The region = lamp the sign is listen to
        /// </summary>
        public Region Region
        {
            get { return region; }
            set { region = value; }
        }

        /// <summary>
        /// The daatobject, to receive the CommunicationAPI data type
        /// </summary>
        public SignData Data
        {
            get
            {
                return new SignData(id, region.Data);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sign"></param>
        public Sign(SignData sign)
        {
            this.id = sign.Id;
            this.region = new Region(sign.Region);
        }
    }
}
