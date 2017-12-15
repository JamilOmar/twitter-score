using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msit.tanalizer.Entity;
using msit.twitterScore.Twitter;

namespace msit.twitterScore.Logic
{
    public class IntegrationLogic
    {
        public void GetAllTwitterElements(string query)
        {

            using (TwitterConnectivity twt = new TwitterConnectivity())
            {
                List<Tweet> tweetList = new List<Tweet>();
                twt.Authenticated();
                var data = twt.GetUserSearch(query, 100, 17000, 451842941779058688);
               
            }
        }
    }
}
