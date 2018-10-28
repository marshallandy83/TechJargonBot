using System;
using System.Configuration;
using System.Threading;
using Common.ItemSelection;
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
				minimumTimeBeforeNextTweet: new TimeSpan(hours: 1, minutes: 0, seconds: 0),
				maximumTimeBeforeNextTweet: new TimeSpan(hours: 2, minutes: 0, seconds: 0));

		private static ISelector<TweetFactory> _tweetFactorySelector =
			new Common.ItemSelection.Weighted.Selector<TweetFactory>(
				new Common.ItemSelection.Weighted.Item<TweetFactory>[]
				{
					new Common.ItemSelection.Weighted.Item<TweetFactory>(
						contents: new TweetFactory.StatusUpdateFactory(_twitterContext),
						weighting: 1),
					new Common.ItemSelection.Weighted.Item<TweetFactory>(
						contents: new TweetFactory.ReplyFactory(
							_twitterContext,
							new Twitter.AllWordsQueryFactory(),
							new Twitter.TweetFinder(_twitterContext)),
						weighting: 7)
				});

		static void Main(String[] args)
		{
			while (true)
			{
				String tweet = _tweetFactorySelector.Select().SendTweet();

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
