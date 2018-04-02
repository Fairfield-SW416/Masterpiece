using System;
using System.Diagnostics.Contracts;
using LinqToTwitter;
using System.Linq;

namespace Masterpiece.Services
{
    public class TwitterService
    {
        public TwitterService()
        {
            var authorizer = GetTwitterAuthorizer();
            getTWitter(authorizer);
        }

        private IAuthorizer GetTwitterAuthorizer()
        {
            // See https://apps.twitter.com/app/7876905/keys for secrets.
            Contract.Ensures(Contract.Result<IAuthorizer>() != null);
            var credentialStore = new SingleUserInMemoryCredentialStore
            {
                ConsumerKey = "scS2r4sCihglH0IGOE9mtp9vN",
                ConsumerSecret = "NOT IN REPO",
                AccessToken = "24472612-twnsStRgRLtTf0Q4hRj8AsOUxXRN2YFrSwqQUmNly",
                AccessTokenSecret = "NOT IN REPO"
            };

            var auth = new SingleUserAuthorizer
            {
                CredentialStore = credentialStore
            };

            return auth;
        }

        private void getTWitter(IAuthorizer authorizer)
        {
            try
            {
                using (var twitterCtx = new TwitterContext(authorizer))
                {
                    //Log
                    twitterCtx.Log = Console.Out;

                    HomeStatusQueryDemo(twitterCtx);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Shows how to get statuses for logged-in user's friends, including retweets
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private void HomeStatusQueryDemo(TwitterContext twitterCtx)
        {
            var tweets =
                (from tweet in twitterCtx.Status
                 where tweet.Type == StatusType.User
                 select tweet)
                .ToList();

            Console.WriteLine("\nTweets for " + twitterCtx.User.ToString() + "\n");
            foreach (var tweet in tweets)
            {
                Console.WriteLine(
                    "Friend: " + tweet.User.ScreenName +
                    "\nRetweeted by: " +
                        (tweet.Retweeted ?
                           tweet.RetweetedStatus.User.Name :
                           "Original Tweet") +
                    "\nTweet: " + tweet.Text + "\n");
            }
        }
    }
}
