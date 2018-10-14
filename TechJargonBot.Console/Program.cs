using System;
using System.Configuration;
using System.Threading;
using LinqToTwitter;
using TechJargonBot.Business;

namespace TechJargonBot.Console
{
	class Program
	{
		private static TwitterContext _twitterContext =
			new TwitterContext(
				new SingleUserAuthorizer
				{
					CredentialStore = new InMemoryCredentialStore()
					{
						ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"],
						ConsumerSecret = ConfigurationManager.AppSettings["ConsumerKeySecret"],
						OAuthToken = ConfigurationManager.AppSettings["AccessToken"],
						OAuthTokenSecret = ConfigurationManager.AppSettings["AccessTokenSecret"],
					}
				});

		private static TimingHandler _timingHandler =
			new TimingHandler(
				minimumTimeBeforeNextTweet: new TimeSpan(hours: 0, minutes: 45, seconds: 0),
				maximumTimeBeforeNextTweet: new TimeSpan(hours: 1, minutes: 30, seconds: 0));

		private static readonly TweetFactory[] TweetFactories = new TweetFactory[]
		{
			//new TweetFactory.StatusUpdateFactory(_twitterContext),
			new TweetFactory.ReplyFactory(
				_twitterContext,
				new Twitter.AllWordsQueryFactory(),
				new Twitter.TweetFinder(_twitterContext))
		};

		static void Main(String[] args)
		{
			while (true)
			{
				String tweet =
					TweetFactories
					.PickAtRandom()
					.SendTweet();

				DateTime nextTweetTime = _timingHandler.GetNextTweetTime();

				OutputToConsole(tweet, nextTweetTime);

				Thread.Sleep(nextTweetTime - DateTime.Now);
			}
		}

		private static void OutputToConsole(
			String sentence,
			DateTime nextTweetTime)
		{
			System.Console.WriteLine(sentence);
			System.Console.WriteLine($"The next tweet will be at {nextTweetTime.ToShortTimeString()}.");
		}
	}
}
