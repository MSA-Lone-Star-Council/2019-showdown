using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Common;
using Tweetinvi;
using Tweetinvi.Events;
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

            var twitterCredentials = new TwitterCredentials(
                Secrets.twitterConsumerKey, 
                Secrets.twitterConsumerSecret,
                Secrets.twitterAccessToken, 
                Secrets.twitterAccessTokenSecret)
            {
                ApplicationOnlyBearerToken = Secrets.twitterBearerToken
            };
            Auth.SetCredentials(twitterCredentials);
        }

        public async Task OnBegin()
        {
            SetTwitterCredentials();
            filteredStream.AddTrack(TRACK_HASHTAG);
            await GetCachedTweets();
            List<string> strings = new List<string>
            {
                "test"
            };
            var stream = Stream.CreateFilteredStream();
            stream.AddTrack("#blackpanther");
            stream.MatchingTweetReceived += AddTweetInRealtime();
            stream.StartStreamMatchingAnyCondition();

        }

        //Something like this, where we specify this event handler is to be implemented by  the platform specific classes
        public EventHandler<MatchedTweetReceivedEventArgs> AddTweetInRealtime()
        {
            throw new NotImplementedException();
        }

        public void OnClickRow(ITweet Tweet)
        {
            View.OpenTweet(Tweet);
        }

        private Task UpdateFromServer()
        {
            throw new NotImplementedException();
        }

        private async Task GetCachedTweets()
        {
            //TODO Make a local model of Itweet
            //TODO Update Backend so it actually does this method
            /*
            tweets = await _client.GetTweetsAsync();    
            if (View != null) View.Tweets = tweets;
            */
            await Task.CompletedTask;
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