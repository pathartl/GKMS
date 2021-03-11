using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class TheSimsLivinLarge : RegistryGame
    {
        public override string Name { get { return "The Sims: Livin' Large"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\SOFTWARE\Electronic Arts\Maxis\The Sims Livin' Large\ergc"; } }
        public override string RegistryValueName { get { return ""; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
