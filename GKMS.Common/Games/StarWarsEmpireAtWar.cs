using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common.Games
{
    public class StarWarsEmpireAtWar : RegistryGame
    {
        public override string Name { get { return "Star Wars: Empire At War"; } }
        public override string RegistryPath { get { return @"HKEY_LOCAL_MACHINE\SOFTWARE\LucasArts\Star Wars Empire at War\1.0"; } }
        public override string RegistryValueName { get { return "CD Key"; } }
        public override bool CanUseWow6432Node { get { return true; } }
    }
}
