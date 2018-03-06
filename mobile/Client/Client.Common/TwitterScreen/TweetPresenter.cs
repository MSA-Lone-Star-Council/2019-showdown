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
        private readonly IClientUi _clientUi;
        private static readonly string TRACK_HASHTAG = "#blackpanther";

        List<ITweet> tweets;

        public TweetPresenter(ShowdownRESTClient client, IClientUi clientUi)
        {
            _client = client;
            _clientUi = clientUi;
            filteredStream = Stream.CreateFilteredStream();
            SetTwitterCredentials();
        }

        public async Task OnBegin()
        {
            filteredStream.AddTrack(TRACK_HASHTAG);
            await GetCachedTweets();
            filteredStream.AddTrack(TRACK_HASHTAG);
            filteredStream.MatchingTweetReceived += (sender, arg) =>
            {
                _clientUi.updateClientUi();
            };
            filteredStream.StartStreamMatchingAnyCondition();

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
            Secrets.twitterConsumerKey,
            Secrets.twitterConsumerSecret,
            Secrets.twitterAccessToken,
            Secrets.twitterAccessTokenSecret)
            {
                ApplicationOnlyBearerToken = Secrets.twitterBearerToken
            };
            Auth.SetCredentials(twitterCredentials);
        }
    }
}