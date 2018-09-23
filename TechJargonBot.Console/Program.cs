using System;
using System.Configuration;
using System.Threading;
using TechJargonBot.Business;
using TechJargonBot.Business.Data;
using LinqToTwitter;
using System.Collections.ObjectModel;

namespace TechJargonBot.Console
{
	class Program
	{
		private const Int32 MinimumNumberOfHoursBetweenTweets = 3;
		private const Int32 MaximumNumberOfHoursBetweenTweets = 5;
		private const Int32 MinimumNumberOfMinutesBetweenTweets = 0;
		private const Int32 MaximumNumberOfMinutesBetweenTweets = 59;

		private static readonly TweetFactory[] TweetFactories = new []
		{
			new TweetFactory.StatusUpdateFactory()
		};

		static void Main(String[] args)
		{
			var twitterContext =
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

			var randomNumberGenerator = new Random();
			var wordDataProvider = new DataProvider(dataReader: new CsvFileReader());
			var sentenceDataProvider = new RandomSentenceProvider(randomNumberGenerator);

			Generator sentenceGenerator =
				CreateGenerator(
					sentenceDataProvider,
					wordDataProvider,
					randomNumberGenerator);

			while (true)
			{
				String tweet =
					TweetFactories
					.PickAtRandom(randomNumberGenerator)
					.CreateTweet(sentenceGenerator);

				TimeSpan timeUntilNextTweet = GetRandomTimeSpan(randomNumberGenerator);

				SendTweet(twitterContext, tweet);
				OutputToConsole(tweet, GetNextTweetTime(timeUntilNextTweet));

				Thread.Sleep(timeUntilNextTweet);
			}
		}

		private static Generator CreateGenerator(
			ISentenceProvider sentenceDataProvider,
			IDataProvider wordDataProvider,
			Random randomNumberGenerator)
		{
			return
				new Generator(
					sentenceDataProvider,
					wordSelector:
						new RegularWordSelector(
							wordProvider: new RandomWordProvider(wordDataProvider, randomNumberGenerator),
							stringFormatter: new RegularStringFormatter()),
					stringFormatter: new RegularStringFormatter());
		}

		private static TimeSpan GetRandomTimeSpan(Random randomNumberGenerator)
		{
			return
				new TimeSpan(
					hours: randomNumberGenerator.Next(
						MinimumNumberOfHoursBetweenTweets,
						MaximumNumberOfHoursBetweenTweets),
					minutes: randomNumberGenerator.Next(
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

		private async static void SendTweet(
			TwitterContext twitterContext,
			String sentence)
		{
			var result =
				await twitterContext.TweetAsync(sentence);
		}
	}
}
