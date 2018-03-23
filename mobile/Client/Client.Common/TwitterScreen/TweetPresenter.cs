using System;
using System.Web;
using Common.Common;

namespace Client.Common
{
	public class TweetPresenter : Presenter<ITweetView>
	{

		private UriBuilder uriBuilder;
		private static readonly string HASHTAG_PRIMARY = "#rockets"; //TODO: Change later to Showdown tags
		private static readonly string HASHTAG_SECONDARY = "#lakers"; //TODO: Change later to Showdown tags
		private static readonly string BASE_SEARCH_URL = "https://twitter.com/search";

		public TweetPresenter()
		{
			uriBuilder = new UriBuilder(BASE_SEARCH_URL);
		}

		public void OnBegin()
		{
			if (View.HasInternetConnection())
			{
				View.StartWebView(BuildUriQuery().ToString());
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
			queryBuilder["q"] = HASHTAG_PRIMARY + ", OR " + HASHTAG_SECONDARY;
			queryBuilder["src"] = "typd";
			uriBuilder.Query = queryBuilder.ToString();
			return uriBuilder.Uri;
		}
	}
}
