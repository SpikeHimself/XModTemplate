namespace XModTemplate.RPC
{
    internal static class ClientEvents
    {
        /// <summary>
        /// The server denied a request
        /// </summary>
        /// <param name="sender">The server</param>
        /// <param name="request">The text of the request</param>
		/// <param name="reason">The reason why the server denied the request</param>
        internal static void RPC_ExampleRequestDenied(long sender, string request, string reason)
        {
            Jotunn.Logger.LogDebug($"The server has denied request `{request}`, with reason `{reason}` :(");
            XModTemplate.ExampleRequestDenied(reason);
        }

        /// <summary>
        /// The server granted a request
        /// </summary>
        /// <param name="sender">The server</param>
        /// <param name="request">The text of the request</param>
        internal static void RPC_ExampleRequestGranted(long sender, string request)
        {
            Jotunn.Logger.LogDebug($"The server has granted request `{request}` :)");
            XModTemplate.ExampleRequestGranted();
        }
    }
}
