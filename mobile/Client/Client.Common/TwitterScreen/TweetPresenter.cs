using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Common;
using Tweetinvi;
using Tweetinvi.Events;
using Tweetinvi.Models;

namespace Client.Common
{
    public class TweetPresenter : Presenter<ITweetView>
    {
        private readonly ShowdownRESTClient _client;

        List<ITweet> tweets;

        public TweetPresenter(ShowdownRESTClient client)
        {
            _client = client;

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

        //Add a lambda representing Platform specific update UI thread
        // Or, just have them inherit this method, call super, and then update UI thread
        public async Task OnTick() 
        {
            await UpdateFromServer();
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
    }
}