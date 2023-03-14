namespace XModTemplate.RPC
{
    internal static class SendToClient
    {
        /// <summary>
        /// Deny a request
        /// </summary>
		/// <param name="clientPeerID">The client whose request is being denied</param>
        /// <param name="request">The text of the request being denied</param>
		/// <param name="reason">The reason why the request is being denied</param>
        internal static void RequestDenied(long clientPeerID, string request, string reason)
        {
            Jotunn.Logger.LogDebug($"Denying request `{request}` with reason `{reason}`");
            ZRoutedRpc.instance.InvokeRoutedRPC(clientPeerID, RPCManager.RPC_EXAMPLEREQUESTDENIED, request, reason);
        }

        /// <summary>
        /// Grant a request
        /// </summary>
        /// <param name="clientPeerID">The client whose request is being granted</param>
        /// <param name="request">The text of the request being granted</param>
        internal static void RequestGranted(long clientPeerID, string request)
        {
            Jotunn.Logger.LogDebug($"Granting request `{request}`");
            ZRoutedRpc.instance.InvokeRoutedRPC(clientPeerID, RPCManager.RPC_EXAMPLEREQUESTGRANTED, request);
        }
    }
}
