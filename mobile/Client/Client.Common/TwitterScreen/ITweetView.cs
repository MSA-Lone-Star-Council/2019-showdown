namespace Client.Common
{
    public interface ITweetView
    {
        void StartWebView(string url);
        void NoInternetConnection();
    }
}
