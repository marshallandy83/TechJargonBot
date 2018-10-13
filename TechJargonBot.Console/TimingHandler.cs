using System;

namespace TechJargonBot.Console
{
	internal class TimingHandler
	{
		private static readonly TimeSpan _minimumTimeBeforeNextTweet = new TimeSpan(hours: 0, minutes: 45, seconds: 0);
		private static readonly TimeSpan _maximumTimeBeforeNextTweet = new TimeSpan(hours: 2, minutes: 0, seconds: 0);

		private Random _randomTimeSpanGenerator = new Random();

		public DateTime GetNextTweetTime() =>
			DateTime.Now.AddMilliseconds(
				_randomTimeSpanGenerator.Next(
					(Int32)_minimumTimeBeforeNextTweet.TotalMilliseconds,
					(Int32)_maximumTimeBeforeNextTweet.TotalMilliseconds));
	}
}