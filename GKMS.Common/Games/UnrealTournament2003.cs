using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class UnrealTournament2003 : RegistryGame
    {
        public override string Name { get { return "Unreal Tournament 2003"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\SOFTWARE\Unreal Technology\Installed Apps\UT2003"; } }
        public override string RegistryValueName { get { return "CDKey"; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
