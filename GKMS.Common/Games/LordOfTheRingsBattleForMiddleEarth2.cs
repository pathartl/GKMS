using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class LordOfTheRingsBattleForMiddleEarth2 : RegistryGame
    {
        public override string Name { get { return "LOTR: The Battle for Middle-Earth II"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\Software\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc"; } }
        public override string RegistryValueName { get { return ""; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
