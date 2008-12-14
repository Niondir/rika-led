using System;
using System.Collections.Generic;
using System.Text;
using StoreServer.Data;

namespace StoreServer.Radio
{
    public class SetTextPacket : LampPacket
    {
        public SetTextPacket(Product product)
            : base(LampCommand.SetText, product.Sign.Id.ToString("0000"), product.Name + ": " + product.Price.ToString())
        {
        }
    }
}
