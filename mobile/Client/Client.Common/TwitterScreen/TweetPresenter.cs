using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Common;
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
        }

        public async Task OnBegin()
        {
            await UpdateFromServer();
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

        private async Task UpdateFromServer()
        {
            tweets = await _client.GetTweetsAsync();    //TODO Make a local model of Itweet

            if (View != null)
                View.Tweets = tweets;
        }
    }
}