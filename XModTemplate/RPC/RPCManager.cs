using System;

namespace XModTemplate.RPC
{
    internal static class RPCManager
    {
        #region RPC Names
        // Server RPCs
        internal const string RPC_EXAMPLEREQUESTDENIED = Mod.Info.Name + "_RequestDenied";
        internal const string RPC_EXAMPLEREQUESTGRANTED = Mod.Info.Name + "_RequestGranted";

        // Client RPCs
        internal const string RPC_EXAMPLEREQUEST = Mod.Info.Name + "_Request";
        #endregion

        /// <summary>
        /// Register our RPCs with ZRoutedRpc, so that the game knows which function to call when these messages arrive
        /// </summary>
        internal static void Register()
        {
            // Server RPCs
            ZRoutedRpc.instance.Register(RPC_EXAMPLEREQUESTDENIED, new Action<long, string, string>(ClientEvents.RPC_ExampleRequestDenied));
            ZRoutedRpc.instance.Register(RPC_EXAMPLEREQUESTGRANTED, new Action<long, string>(ClientEvents.RPC_ExampleRequestGranted));

            // Client RPCs
            ZRoutedRpc.instance.Register(RPC_EXAMPLEREQUEST, new Action<long, string>(ServerEvents.RPC_ExampleRequest));
        }
    }
}
