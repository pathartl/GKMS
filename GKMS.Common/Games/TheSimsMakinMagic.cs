using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class TheSimsMakinMagic : RegistryGame
    {
        public override string Name { get { return "The Sims: Makin' Magic"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\SOFTWARE\Electronic Arts\Maxis\The Sims Makin' Magic\ergc"; } }
        public override string RegistryValueName { get { return ""; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
