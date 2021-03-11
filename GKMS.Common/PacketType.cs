using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common
{
    public enum PacketType
    {
        Error,
        LocateServer,
        LocateClients,
        ServerLocated,
        ClientLocated,
        ServerAllocateKey,
        ClientChangeKey,
        ClientGetKey,
        NoAvailableKeys
    }
}
