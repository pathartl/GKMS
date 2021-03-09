using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class FreedomForce : RegistryGame
    {
        public override string Name { get { return "Freedom Force"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\Software\Electronic Arts\EA Distribution\Freedom Force\ergc"; } }
        public override string RegistryValueName { get { return ""; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
