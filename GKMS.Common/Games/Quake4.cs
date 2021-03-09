using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class Quake4 : RegistryGame
    {
        public override string Name { get { return "Quake 4"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\SOFTWARE\id\Quake 4"; } }
        public override string RegistryValueName { get { return "CDKey"; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
