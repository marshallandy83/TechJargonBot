using System;
using System.Configuration;
using System.Threading;
using TechJargonBot.Business;
using TechJargonBot.Business.Data;
using Twitter;
//test
namespace TechJargonBot.Console
{
	class Program
	{
		private const Int32 MinimumNumberOfHoursBetweenTweets = 3;
		private const Int32 MaximumNumberOfHoursBetweenTweets = 5;
		private const Int32 MinimumNumberOfMinutesBetweenTweets = 0;
		private const Int32 MaximumNumberOfMinutesBetweenTweets = 59;

		static void Main(String[] args)
		{
			var twitterApi =
				new TwitterApi(
					ConfigurationManager.AppSettings["ConsumerKey"],
					ConfigurationManager.AppSettings["ConsumerKeySecret"],
					ConfigurationManager.AppSettings["AccessToken"],
					ConfigurationManager.AppSettings["AccessTokenSecret"]);

			var randomNumberGenerator = new Random();
			var wordDataProvider = new DataProvider(dataReader: new CsvFileReader());
			var sentenceDataProvider = new RandomSentenceProvider(randomNumberGenerator);

			while (true)
			{
				String sentence =
					CreateGenerator(
						sentenceDataProvider,	
						wordDataProvider,
						randomNumberGenerator)
					.Generate();

				TimeSpan timeUntilNextTweet = GetRandomTimeSpan(randomNumberGenerator);

				SendTweet(twitterApi, sentence);
				OutputToConsole(sentence, GetNextTweetTime(timeUntilNextTweet));

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
			TwitterApi twitterApi,
			String sentence)
		{
			await twitterApi.Tweet(sentence);
		}
	}
}
