﻿using Battleships.Framework.Networking;
using Battleships.Framework.Networking.Serialization;

namespace Battleships.Messages
{
    internal struct TestMessage : INetworkMessage
    {
        public void Serialize(ref NetworkWriter writer)
        {
            writer.Write(12345);
        }

        public void Deserialize(ref NetworkReader reader)
        {
        }
    }
}
