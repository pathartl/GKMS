using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class Battlefield1942SecretWeaponsOfWWII : RegistryGame
    {
        public override string Name { get { return "Battlefield 1942: Secret Weapons of WWII"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\SOFTWARE\Electronic Arts\EA GAMES\Battlefield Vietnam\ergc"; } }
        public override string RegistryValueName { get { return ""; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
