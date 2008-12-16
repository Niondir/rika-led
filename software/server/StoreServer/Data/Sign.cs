using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.DataTypes;

namespace StoreServer.Data
{
    public class Sign
    {
        private int id;
        private Region region;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Region Region
        {
            get { return region; }
            set { region = value; }
        }

        public SignData Data
        {
            get
            {
                return new SignData(id, region.Data);
            }
        }

        public Sign(SignData sign)
        {
            this.id = sign.Id;
            this.region = new Region(sign.Region);
        }
    }
}
