using System;
using System.Collections.Generic;
using System.Text;
using Tweetinvi.Models;

namespace Client.Common
{
    public interface IClientUpdateUi
    {
        void UpdateUi(ITweet tweet);
    }
}
