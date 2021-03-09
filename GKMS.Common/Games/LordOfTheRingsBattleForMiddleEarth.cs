using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class LordOfTheRingsBattleForMiddleEarth : RegistryGame
    {
        public override string Name { get { return "LOTR: The Battle for Middle-Earth"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\Software\Electronic Arts\EA GAMES\The Battle for Middle-earth\ergc"; } }
        public override string RegistryValueName { get { return ""; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
