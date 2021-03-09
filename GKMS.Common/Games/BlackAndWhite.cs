using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class BlackAndWhite : RegistryGame
    {
        public override string Name { get { return "Black & White"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\Software\Electronic Arts\EA GAMES\Black and White\ergc"; } }
        public override string RegistryValueName { get { return ""; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
