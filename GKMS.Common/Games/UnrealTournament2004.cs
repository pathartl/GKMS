using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class UnrealTournament2004 : RegistryGame
    {
        public override string Name { get { return "Unreal Tournament 2004"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\SOFTWARE\Unreal Technology\Installed Apps\UT2004"; } }
        public override string RegistryValueName { get { return "CDKey"; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
