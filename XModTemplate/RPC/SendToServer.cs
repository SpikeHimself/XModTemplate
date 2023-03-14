namespace XModTemplate.RPC
{
    internal static class SendToServer
    {
        /// <summary>
        /// Send a request to the server
        /// </summary>
        /// <param name="request">The text of the request</param>
        internal static void ExampleRequest(string request)
        {
            Jotunn.Logger.LogDebug($"Sending request `{request}` to the server");
            ZRoutedRpc.instance.InvokeRoutedRPC(Environment.ServerPeerId, RPCManager.RPC_EXAMPLEREQUEST, request);
        }
    }
}
