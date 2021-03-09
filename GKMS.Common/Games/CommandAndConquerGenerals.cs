using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class CommandAndConquerGenerals : RegistryGame
    {
        public override string Name { get { return "Command & Conquer: Generals"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\SOFTWARE\Electronic Arts\EA Games\Generals\ergc"; } }
        public override string RegistryValueName { get { return ""; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
