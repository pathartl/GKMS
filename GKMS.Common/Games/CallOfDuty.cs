using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class CallOfDuty : RegistryGame
    {
        public override string Name { get { return "Call of Duty"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty"; } }
        public override string RegistryValueName { get { return "codkey"; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
