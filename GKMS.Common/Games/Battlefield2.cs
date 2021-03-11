using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class Battlefield2 : RegistryGame
    {
        public override string Name { get { return "Battlefield 2"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\SOFTWARE\Electronic Arts\EA GAMES\Battlefield 2\ergc"; } }
        public override string RegistryValueName { get { return ""; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
