using BepInEx.Configuration;
using System;

namespace XModTemplate
{
    public class XConfig
    {
        public const string Key_ContainerName = Mod.Info.Name + "_Name";

        ////////////////////////////
        //// Singleton instance ////
        private static readonly Lazy<XConfig> lazy = new Lazy<XConfig>(() => new XConfig());
        public static XConfig Instance { get { return lazy.Value; } }
        ////////////////////////////

        private ConfigFile configFile;
        public ConfigEntry<int> ExampleEntry;

        /// <summary>
        /// Load the config file, and track the settings inside it
        /// </summary>
        /// <param name="configFile">The config file being loaded</param>
        public void LoadLocalConfig(ConfigFile configFile)
        {
            this.configFile = configFile;

            // Add Nexus ID to config for Nexus Update Check (https://www.nexusmods.com/valheim/mods/102)
            configFile.Bind("General", "NexusID", Mod.Info.NexusId, "Nexus mod ID for updates (do not change)");

            ExampleEntry = configFile.Bind("Example Section", "ExampleEntry", 1, "This is an example value. It does nothing.");

            //this.configFile.ConfigReloaded += LocalConfigChanged;
            //this.configFile.SettingChanged += LocalConfigChanged;
        }
    }
}
