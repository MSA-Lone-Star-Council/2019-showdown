using System;
using System.Threading.Tasks;
using System.Web;
using Common.Common;

namespace Client.Common
{
	public class TweetPresenter : Presenter<ITweetView>
	{
		private UriBuilder uriBuilder;
		private static readonly string HASHTAG_PRIMARY = "#TxShowdown18";
		private static readonly string BASE_SEARCH_URL = "https://twitter.com/search";

        private readonly ShowdownRESTClient _client;

        public TweetPresenter(ShowdownRESTClient client)
		{
			uriBuilder = new UriBuilder(BASE_SEARCH_URL);
            _client = client;
		}

		public async Task OnBegin()
		{
            if (Connectivity.IsConnected())
			{
                var isBackendImplemented = await Connectivity.IsBackendReachable("/twitter /query");

                String query;
                if (isBackendImplemented) 
                {
                    query = await _client.GetTwitterQueryAsync();
                } else 
                {
                    query = BuildUriQuery().ToString();
                }
                View.StartWebView(query);				
			}
			else
			{
				View.NoInternetConnection();
			}
		}

		private Uri BuildUriQuery()
		{
			// TODO: Restrict search dates to Showdown dates by modifiying query
			var queryBuilder = HttpUtility.ParseQueryString(uriBuilder.Query);
			queryBuilder["f"] = "tweets";
			queryBuilder["vertical"] = "default";
			queryBuilder["q"] = HASHTAG_PRIMARY;
			queryBuilder["src"] = "typd";
			uriBuilder.Query = queryBuilder.ToString();
			return uriBuilder.Uri;
		}
	}
}
