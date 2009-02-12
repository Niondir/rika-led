using System;
using System.Collections.Generic;
using System.Text;
using StoreServer.Data;
using CommunicationAPI.Radio;


namespace StoreServer.Radio
{
    /// <summary>
    /// Tell the lamp to broadcast a price for a single sign to the signs
    /// </summary>
    public class SetPricePacket : LampPacket
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public SetPricePacket(Product product)
            : base(LampCommand.SetPrice, product.Sign.Id.ToString(), product.Name, product.Price.ToString())
        {
            this.targetId = product.Sign.Region.Id.ToString();
        }

    }
}
