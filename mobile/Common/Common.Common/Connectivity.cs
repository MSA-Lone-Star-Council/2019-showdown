using System;
namespace Common.Common
{
    public static class Connectivity
    {
        public static bool IsConnected()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        public static async Task<bool> IsBackendReachable()
        {
            var connectivity = CrossConnectivity.Current;
            if (!connectivity.IsConnected) return false;

            var reachable = await connectivity.IsRemoteReachable(Secrets.BACKEND_URL);

            return reachable;
        }

    }
}
