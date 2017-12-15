using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;
using msit.tanalizer.Entity;
using msit.twitterScore.Utils;
namespace msit.twitterScore.Twitter
{
    public class TwitterConnectivity:IDisposable
    {
        IAuthorizer authenticationToken;

        public IAuthorizer AuthenticationToken
        {
            get { return authenticationToken; }
            set { authenticationToken = value; }
        }
        TwitterContext twitterContext;

        public TwitterContext TwitterContext
        {
            get { return twitterContext; }
            set { twitterContext = value; }
        }


        public void Authenticated()
        {
            AuthenticationToken = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = Properties.Twitter.Default.CONSUMER_KEY,
                    ConsumerSecret = Properties.Twitter.Default.CONSUMER_SECRET,
                    OAuthToken = Properties.Twitter.Default.ACCESS_TOKEN,
                    OAuthTokenSecret = Properties.Twitter.Default.ACCESS_TOKEN_SECRET
                }
            };
            TwitterContext = new TwitterContext(AuthenticationToken);
        }


        public List<Tweet> GetUserSearch(string query, uint maxItems, uint maxPages, ulong sinceID)
        {
            List<Tweet> tweetList = new List<Tweet>();
            List<Status> StatusList = new List<Status>();
            Search searchResponse = null;
            List<Tweet> data = new List<Tweet>();
            ulong maxIdQuery = 0;
            searchResponse =
                  (from search in TwitterContext.Search
                   where search.Type == SearchType.Search
                   && search.Query == query
                   && search.SinceID == sinceID
                   && search.Count == maxItems
                   select search)
                  .SingleOrDefault();
            if (searchResponse != null && searchResponse.Statuses != null)
                StatusList.AddRange(searchResponse.Statuses);
            maxIdQuery = searchResponse.Statuses.Min(x => x.StatusID) - 1;
            do
            {
                searchResponse =
                (from search in TwitterContext.Search
                 where search.Type == SearchType.Search &&
                       search.Query == query
                       && search.Count == maxItems
                       && search.SinceID == sinceID
                       && search.MaxID == maxIdQuery
                 select search)
                .SingleOrDefault();
                if (searchResponse.Statuses.Count > 0)
                {
                    StatusList.AddRange(searchResponse.Statuses);
                    maxIdQuery = searchResponse.Statuses.Min(x => x.StatusID) - 1;
                }
            }
            while (  searchResponse.Statuses.Count() != 0 && StatusList.Count < maxPages);
            StatusList.ForEach(tweet =>
                 tweetList.Add(new Tweet { ID = tweet.StatusID, ScreenName = tweet.User.Name, Text = tweet.Text, CreatedAt = tweet.CreatedAt, InReplyToScreenName = tweet.InReplyToScreenName, InReplyToStatusID = tweet.InReplyToStatusID, RetweetCount = tweet.RetweetCount, Lang = tweet.Lang })
                );
            return tweetList;
        }
        public void Dispose()
        {
            TwitterContext.Dispose();
        }
    }
}
