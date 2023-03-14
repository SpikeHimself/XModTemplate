namespace Mod
{
    /// <summary>
    /// This is *the* place to edit plugin details. Everywhere else will be generated based on this info.
    /// </summary>
    public static class Info
    {
        // These speak for themselves, right?
        public const string Name = "XModTemplate";
        public const string Version = "1.0.0";
        public const string Description = "This is a template. It does nothing.";

        // Change this to something unique using reverse domain name notation
        // https://en.wikipedia.org/wiki/Reverse_domain_name_notation
        public const string GUID = "yay.spikehimself.xmodtemplate";

        // This should be your username on Thunderstore, not mine!
        public const string Author = "SpikeHimself";

        // Don't forget to update this after you've made a Nexus Mods page for your mod!
        public const int NexusId = -1;

        // Just in case your mod name does not match the GitHub repo it's in
        public const string GitHubRepo = "SpikeHimself/XModTemplate";

        // If you have your own website, use that. Otherwise, GitHub is fine.
        public const string WebsiteUrl = "https://github.com/" + GitHubRepo;

        // Don't change these!
        public const string HarmonyGUID = GUID + ".harmony";
        public const string JotunnVersion = Jotunn.Main.Version;

        // BepInEx does not expose its version number in a const. Maybe one day..
        public const string BepInExPackVersion = "5.4.2100";

    }
}
