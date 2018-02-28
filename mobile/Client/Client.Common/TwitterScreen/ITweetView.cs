using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Models;

namespace Client.Common
{
    public interface ITweetView
    {
        List<ITweet> Tweets { get; set; }

        void OpenTweet(ITweet tweet);

        void Refresh();
    }
}
