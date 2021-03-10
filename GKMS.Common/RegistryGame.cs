using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace GKMS.Common
{
    public abstract class RegistryGame : IGame
    {
        public abstract string Name { get; }
        public abstract string RegistryPath { get; }
        public abstract string RegistryValueName { get; }
        public abstract bool CanUseWow6432Node { get; }

        public string[] Keys { get; set; }

        public string GetKey()
        {
            RegistryKey baseRegistry;

            if (Environment.Is64BitOperatingSystem && !CanUseWow6432Node)
            {
                baseRegistry = RegistryKey.OpenBaseKey(GetHive(), RegistryView.Registry64);
            }
            else
            {
                baseRegistry = RegistryKey.OpenBaseKey(GetHive(), RegistryView.Registry32);
            }

            var subKey = baseRegistry.OpenSubKey(GetRegistryPath());

            if (subKey == null)
            {
                return "";
            }
            else
            {
                return (string)subKey.GetValue(RegistryValueName);
            }
        }

        public void ChangeKey(string key)
        {
            RegistryKey baseRegistry;

            if (Environment.Is64BitOperatingSystem && !CanUseWow6432Node)
            {
                baseRegistry = RegistryKey.OpenBaseKey(GetHive(), RegistryView.Registry64);
            }
            else
            {
                baseRegistry = RegistryKey.OpenBaseKey(GetHive(), RegistryView.Registry32);
            }

            var subKey = baseRegistry.OpenSubKey(GetRegistryPath(), true);

            if (subKey == null)
            {
                subKey = baseRegistry.CreateSubKey(GetRegistryPath(), true);
            }

            subKey.SetValue(RegistryValueName, key);
        }

        private string GetRegistryPath()
        {
            var match = Regex.Match(RegistryPath, @"HKEY_\w+\\(.+)");

            return match.Groups[1].Value;
        }

        private RegistryHive GetHive()
        {
            var match = Regex.Match(RegistryPath, @"(HKEY_\w+)\\");

            var hiveString = match.Groups[1].Value;

            switch (hiveString)
            {
                case "HKEY_CLASSES_ROOT":
                    return RegistryHive.ClassesRoot;

                case "HKEY_CURRENT_USER":
                    return RegistryHive.CurrentUser;

                case "HKEY_USERS":
                    return RegistryHive.Users;

                case "HKEY_CURRENT_CONFIG":
                    return RegistryHive.CurrentConfig;

                default:
                    return RegistryHive.LocalMachine;
            }
        }
    }
}
