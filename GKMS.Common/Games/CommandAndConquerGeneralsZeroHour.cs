using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class CommandAndConquerGeneralsZeroHour : RegistryGame
    {
        public override string Name { get { return "Command & Conquer: Generals Zero Hour"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\SOFTWARE\Electronic Arts\EA Games\command and conquer generals zero hour\ergc"; } }
        public override string RegistryValueName { get { return "codkey"; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
