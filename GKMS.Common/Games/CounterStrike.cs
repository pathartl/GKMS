using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class CounterStrike : RegistryGame
    {
        public override string Name { get { return "Counter-Strike"; } }
        public override string RegistryPath { get { return @"HKEY_CURRENT_USER\Software\Valve\CounterStrike\Settings"; } }
        public override string RegistryValueName { get { return "Key"; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
