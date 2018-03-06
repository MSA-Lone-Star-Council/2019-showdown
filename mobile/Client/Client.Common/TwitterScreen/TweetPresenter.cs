using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Common;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Streaming;

namespace Client.Common
{
    public class TweetPresenter : Presenter<ITweetView>
    {

        private IFilteredStream filteredStream;
        private TwitterCredentials twitterCredentials;
        private readonly ShowdownRESTClient _client;
        private static readonly string TRACK_HASHTAG = "#test";

        List<ITweet> tweets;

        public TweetPresenter(ShowdownRESTClient client)
        {
            _client = client;
            filteredStream = Stream.CreateFilteredStream();
        }

        public async Task OnBegin()
        {
            SetTwitterCredentials();
            filteredStream.AddTrack(TRACK_HASHTAG);
            await UpdateFromServer();
        }

        public void OnClickRow(ITweet Tweet)
        {
            View.OpenTweet(Tweet);
        }

        private async Task UpdateFromServer()
        {
            tweets = await _client.GetTweetsAsync();    //TODO Make a local model of Itweet

            if (View != null)
                View.Tweets = tweets;
        }

        private void SetTwitterCredentials()
        {
            twitterCredentials = new TwitterCredentials(
            Secrets.twitterConsumerKey, Secrets.twitterConsumerSecret,
            Secrets.twitterAccessToken, Secrets.twitterAccessTokenSecret)
            {
                ApplicationOnlyBearerToken = Secrets.twitterBearerToken
            };
            Auth.SetCredentials(twitterCredentials);
        }

        public void ListenForTweets(IClientUi clientUi)
        {
            filteredStream.MatchingTweetReceived += (sender, arg) =>
            {
                clientUi.updateClientUi();
            };
        }
    }
}