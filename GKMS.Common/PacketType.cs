using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common
{
    public enum PacketType
    {
        LocateServer,
        LocateClients,
        ServerLocated,
        ClientLocated,
        ServerAllocateKey,
        ClientChangeKey,
        ClientGetKey,
    }
}
