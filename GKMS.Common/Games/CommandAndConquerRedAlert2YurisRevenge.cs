using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class CommandAndConquerRedAlert2YurisRevenge : RegistryGame
    {
        public override string Name { get { return "Command & Conquer: Red Alert 2 - Yuri's Revenge"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\SOFTWARE\Westwood\Yuri's Revenge"; } }
        public override string RegistryValueName { get { return "Serial"; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
