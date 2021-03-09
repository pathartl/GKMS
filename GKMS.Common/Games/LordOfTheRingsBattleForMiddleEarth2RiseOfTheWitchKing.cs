using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class LordOfTheRingsBattleForMiddleEarth2RiseOfTheWitchKing : RegistryGame
    {
        public override string Name { get { return "LOTR: The Battle for Middle-Earth II The Rise of the Witch-King"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\Software\Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king\ergc"; } }
        public override string RegistryValueName { get { return ""; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
