using System;

namespace TechJargonBot.Console
{
	internal class TimingHandler
	{
		private readonly Random _randomTimeSpanGenerator = new Random();
		private readonly TimeSpan _minimumTimeBeforeNextTweet;
		private readonly TimeSpan _maximumTimeBeforeNextTweet;

		public TimingHandler(
			TimeSpan minimumTimeBeforeNextTweet,
			TimeSpan maximumTimeBeforeNextTweet)
		{
			_minimumTimeBeforeNextTweet = minimumTimeBeforeNextTweet;
			_maximumTimeBeforeNextTweet = maximumTimeBeforeNextTweet;
		}

		public DateTime GetNextTweetTime() =>
			DateTime.Now.AddMilliseconds(
				_randomTimeSpanGenerator.Next(
					(Int32)_minimumTimeBeforeNextTweet.TotalMilliseconds,
					(Int32)_maximumTimeBeforeNextTweet.TotalMilliseconds));
	}
}