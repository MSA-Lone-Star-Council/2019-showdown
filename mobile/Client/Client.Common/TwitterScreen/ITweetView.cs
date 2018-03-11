namespace Client.Common
{
    public interface ITweetView
    {
        void StartWebView();
        void NoInternetConnection();
        bool HasInternetConnection();
    }
}
