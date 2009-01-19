using System;
using System.Collections.Generic;
using System.Text;
using StoreServer.Data;


namespace StoreServer.Radio
{
    public class SetPricePacket : LampPacket
    {
        public SetPricePacket(Product product)
            : base(LampCommand.SetPrice, product.Sign.Id.ToString(), product.Name, product.Price.ToString())
        {
            this.targetId = product.Sign.Region.Id.ToString();
            // Lampen ID
            //product.Sign.Region.Id
        }

    }
}
