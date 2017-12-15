using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace msit.tanalizer.Entity
{
    [Serializable]
    public class Tweet
    {
        public DateTime CreatedAt { get; set; }
        public string Lang { get; set; }
        public int RetweetCount { get; set; }
 
        public string ScreenName { get; set; }
        public string InReplyToScreenName { get; set; }
        public ulong InReplyToStatusID { get; set; }

        public string TweetUrl
        {
            get
            {
                return string.Format("https://twitter.com/{0}/status/{1}", ScreenName, ID);
            }
        }

        public string Text { get; set; }

        public ulong ID { get; set; }
    }

}
