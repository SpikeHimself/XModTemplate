namespace XModTemplate.RPC
{
    internal static class ServerEvents
    {
        /// <summary>
        /// A client made a request
        /// </summary>
        /// <param name="sender">The client which made a request</param>
        /// <param name="request">The text of the request</param>
        internal static void RPC_ExampleRequest(long sender, string request)
        {
            if (!Environment.IsServer)
            {
                Jotunn.Logger.LogDebug($"{sender} made a request, but I am not the server");
                return;
            }

            Jotunn.Logger.LogDebug($"{sender} made request `{request}` ..");

            bool answer = XModTemplate.ConsiderRequest(request);
            if (answer)
            {
                Jotunn.Logger.LogDebug(" ..and it's been granted!");
                SendToClient.ExampleRequestGranted(sender, request);
            }
            else
            {
                Jotunn.Logger.LogDebug(" ..but it's been denied :(");
                SendToClient.ExampleRequestDenied(sender, request, "Not this time.");
            }
        }
    }
}
