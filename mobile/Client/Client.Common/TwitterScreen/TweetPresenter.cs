using System;
using System.Threading.Tasks;
using Common.Common;

namespace Client.Common
{
	public class TweetPresenter : Presenter<ITweetView>
	{
        private readonly ShowdownRESTClient _client;

        public TweetPresenter(ShowdownRESTClient client)
		{
            _client = client;
		}

        public async Task OnBegin()
        {
            if (Connectivity.IsConnected())
            {
                var query = await _client.GetTwitterQueryAsync();
                View.StartWebView(query);
            }
            else
            {
                View.NoInternetConnection();
            }
        }
	}
}
