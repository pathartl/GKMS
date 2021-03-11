using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class CommandAndConquerRedAlert2 : RegistryGame
    {
        public override string Name { get { return "Command & Conquer: Red Alert 2"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\SOFTWARE\Westwood\Red Alert 2"; } }
        public override string RegistryValueName { get { return "Serial"; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
