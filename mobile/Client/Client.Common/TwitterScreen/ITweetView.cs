using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Events;
using Tweetinvi.Models;

namespace Client.Common
{
    public interface ITweetView
    {
        List<ITweet> Tweets { set; }

        void Refresh();
        void OpenTweet(ITweet tweet);
    }
}
