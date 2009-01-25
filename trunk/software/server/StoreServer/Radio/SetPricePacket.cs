using System;
using System.Collections.Generic;
using System.Text;
using StoreServer.Data;
using CommunicationAPI.Radio;


namespace StoreServer.Radio
{
    public class SetPricePacket : LampPacket
    {
        public SetPricePacket(Product product)
            : base(LampCommand.SetPrice, product.Sign.Id.ToString(), product.Name, product.Price.ToString())
        {
            this.targetId = product.Sign.Region.Id.ToString();
        }

    }
}
