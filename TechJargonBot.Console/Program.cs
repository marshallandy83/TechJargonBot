﻿using System;
using System.Configuration;
using System.Threading;
using LinqToTwitter;
using TechJargonBot.Business;

namespace TechJargonBot.Console
{
	class Program
	{
		private const Int32 MinimumNumberOfHoursBetweenTweets = 3;
		private const Int32 MaximumNumberOfHoursBetweenTweets = 5;
		private const Int32 MinimumNumberOfMinutesBetweenTweets = 0;
		private const Int32 MaximumNumberOfMinutesBetweenTweets = 59;

		private static TwitterContext twitterContext =
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
			new TweetFactory.StatusUpdateFactory(),
			new TweetFactory.ReplyFactory(
				new Twitter.TweetFinder())
		};

		static void Main(String[] args)
		{
			var randomNumberGenerator = new Random();

			while (true)
			{
				String tweet =
					TweetFactories
					.PickAtRandom(randomNumberGenerator)
					.CreateTweet();

				TimeSpan timeUntilNextTweet = GetRandomTimeSpan(randomNumberGenerator);

				SendTweet(twitterContext, tweet);
				OutputToConsole(tweet, GetNextTweetTime(timeUntilNextTweet));

				Thread.Sleep(timeUntilNextTweet);
			}
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
			var result = await twitterContext.TweetAsync(sentence);
		}
	}
}
