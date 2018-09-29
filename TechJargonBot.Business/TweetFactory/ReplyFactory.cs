using System;
using TechJargonBot.Business.Data;

namespace TechJargonBot.Business
{
	partial class TweetFactory
	{
		public class ReplyFactory : TweetFactory
		{
			private readonly Twitter.TweetFinder _tweetFinder;

			public ReplyFactory(Twitter.TweetFinder tweetFinder) : base(new SentenceType.Reply())
			{
				_tweetFinder = tweetFinder;
			}

			public override String CreateTweet()
			{
				return SentenceGenerator.Generate();
			}
		}
	}
}
