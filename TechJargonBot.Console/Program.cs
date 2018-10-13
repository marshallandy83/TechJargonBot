using System;
using System.Configuration;
using System.Threading;
using LinqToTwitter;
using TechJargonBot.Business;

namespace TechJargonBot.Console
{
	class Program
	{
		private const Int32 MinimumNumberOfHoursBetweenTweets = 0;
		private const Int32 MaximumNumberOfHoursBetweenTweets = 0;
		private const Int32 MinimumNumberOfMinutesBetweenTweets = 50;
		private const Int32 MaximumNumberOfMinutesBetweenTweets = 59;

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

				TimeSpan timeUntilNextTweet = GetRandomTimeSpan();

				OutputToConsole(tweet, GetNextTweetTime(timeUntilNextTweet));

				Thread.Sleep(timeUntilNextTweet);
			}
		}

		private static Random _randomTimeSpanGenerator = new Random();

		private static TimeSpan GetRandomTimeSpan()
		{
			return
				new TimeSpan(
					hours: _randomTimeSpanGenerator.Next(
						MinimumNumberOfHoursBetweenTweets,
						MaximumNumberOfHoursBetweenTweets),
					minutes: _randomTimeSpanGenerator.Next(
						MinimumNumberOfMinutesBetweenTweets,
						MaximumNumberOfMinutesBetweenTweets),
					seconds: 0);
		}

		private static DateTime GetNextTweetTime(TimeSpan timeUntilNextTweet)
		{
			return DateTime.Now.Add(timeUntilNextTweet);
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
