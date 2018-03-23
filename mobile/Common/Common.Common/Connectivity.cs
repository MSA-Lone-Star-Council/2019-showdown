using System.Threading.Tasks;
using Plugin.Connectivity;

namespace Common.Common
{
    public static class Connectivity
    {
        public static bool IsConnected()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        public static async Task<bool> IsBackendReachable(string endpoint)
        {
            var connectivity = CrossConnectivity.Current;
            if (!connectivity.IsConnected) return false;

            var reachable = await connectivity.IsRemoteReachable(Secrets.BACKEND_URL + endpoint);

            return reachable;
        }

    }
}
