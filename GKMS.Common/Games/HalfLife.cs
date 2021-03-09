using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class HalfLife : RegistryGame
    {
        public override string Name { get { return "Half-Life"; } }
        public override string RegistryPath { get { return @"HKEY_CURRENT_USER\Software\Valve\Half-Life\Settings"; } }
        public override string RegistryValueName { get { return "Key"; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
