using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class CommandAndConquerTiberianSun : RegistryGame
    {
        public override string Name { get { return "Command & Conquer: Tiberian Sun"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\SOFTWARE\westwood\tiberian sun"; } }
        public override string RegistryValueName { get { return "serial"; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
