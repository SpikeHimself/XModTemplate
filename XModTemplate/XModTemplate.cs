using BepInEx;
using Jotunn.Managers;
using Jotunn.Utils;
using XModTemplate.RPC;

namespace XModTemplate
{
    [BepInDependency(Jotunn.Main.ModGuid)]
    [BepInPlugin(Mod.Info.GUID, Mod.Info.Name, Mod.Info.Version)]
    [NetworkCompatibility(CompatibilityLevel.NotEnforced, VersionStrictness.Patch)]
    public class XModTemplate : BaseUnityPlugin
    {
        #region Unity Events
        /// <summary>
        /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "MonoBehaviour.Awake is called when the script instance is being loaded.")]
        private void Awake()
        {
            // Hello, world!
            Jotunn.Logger.LogDebug("I HAVE ARRIVED!");

            // Load config
            XConfig.Instance.LoadLocalConfig(Config);

            // Apply the Harmony patches
            Patches.Patcher.Patch();

            // Subscribe to Jotunn's OnVanillaMapDataLoaded event
            MinimapManager.OnVanillaMapDataLoaded += MinimapManager_OnVanillaMapDataLoaded;
        }

        /// <summary>
        /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "MonoBehaviour.Update is called every frame, if the MonoBehaviour is enabled.")]
        private void Update()
        {
        }

        /// <summary>
        /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDestroy.html
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "MonoBehaviour.OnDestroy occurs when a Scene or game ends.")]
        private void OnDestroy()
        {
            Patches.Patcher.Unpatch();
        }
        #endregion

        #region Game
        public static void GameStarted()
        {
            RPCManager.Register();
        }
        #endregion

        #region Jotunn events
        /// <summary>
        /// Event that gets fired once data for a specific Map for a world has been loaded.
        /// https://valheim-modding.github.io/Jotunn/tutorials/events.html
        /// </summary>
        private static void MinimapManager_OnVanillaMapDataLoaded()
        {
            Jotunn.Logger.LogInfo("The game has fully loaded; sending an ExampleRequest to the server..");

            var myId = ZDOMan.instance.GetMyID();
            var myName = Game.instance.GetPlayerProfile().GetName();
            SendToServer.ExampleRequest($"{myName} ({myId}) has joined the game, please do a thing");
        }
        #endregion

        #region Determine a thing
        public static bool ConsiderRequest(string request)
        {
            Jotunn.Logger.LogInfo($"Considering answer to request `{request}`..");

            int rnd = UnityEngine.Random.Range(0, 2);
            bool maybe = rnd == 0;
            string answer = maybe ? "yes" : "no";
            Jotunn.Logger.LogInfo($"..the answer is: {answer}");

            return maybe;
        }
        #endregion

        #region RPC events
        public static void ExampleRequestDenied(string reason)
        {
            Jotunn.Logger.LogInfo($"The server denied our request, stating: {reason}");
        }

        public static void ExampleRequestGranted()
        {
            Jotunn.Logger.LogInfo("The server granted our request!");
        }
        #endregion
    }
}
