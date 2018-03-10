using System;
using System.Collections.Generic;
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
        public IClientUpdateUi ClientUpdateUi { get; set; }
        private static readonly string TRACK_HASHTAG = "rockets";

        List<ITweet> tweets;

        public TweetPresenter(ShowdownRESTClient client)
        {
            tweets = new List<ITweet>();
            _client = client;
            SetTwitterCredentials();
            filteredStream = Stream.CreateFilteredStream();
        }

        public async Task OnBegin()
        {
            View.Tweets = tweets;
            //await GetCachedTweets();
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

        public void OnPause()
        {
            filteredStream.PauseStream();
        }

        public void OnResume()
        {
            filteredStream.ResumeStream();
        }

        public void StartStream() {
            filteredStream.AddTrack(TRACK_HASHTAG);
            filteredStream.MatchingTweetReceived += (sender, arg) =>
            {   if (!arg.Tweet.Retweeted && !arg.Tweet.Text.Contains("RT"))
                {
                    ClientUpdateUi.UpdateUi(arg.Tweet);
                }
            };
            filteredStream.StartStreamMatchingAnyCondition();
        }
    }
}