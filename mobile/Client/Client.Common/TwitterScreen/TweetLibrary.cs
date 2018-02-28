using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Streaming;
using Tweetinvi.Models;
using Common.Common;

namespace Client.Common.TwitterScreen
{
    public class TweetLibrary
    {
        List<String> tagsToTrack;
        IFilteredStream filteredStream;
        TwitterCredentials twitterCredentials;

        public IFilteredStream FilteredStream { get => filteredStream; }

        public TweetLibrary(List<String> tagsToTrack)
        {
            this.tagsToTrack = tagsToTrack;
            filteredStream = Stream.CreateFilteredStream();
        }

        public void SetTwitterCredentials()
        {
            twitterCredentials = new TwitterCredentials(
            Secrets.twitterConsumerKey, Secrets.twitterConsumerSecret,
            Secrets.twitterAccessToken, Secrets.twitterAccessTokenSecret)
            {
                ApplicationOnlyBearerToken = Secrets.twitterBearerToken
            };
            Auth.SetCredentials(twitterCredentials);
        }

        public void AddStreamTracks()
        {
            foreach (string track in tagsToTrack)
            {
                filteredStream.AddTrack(track);
            }
        }
    }
}
