using System;

namespace TechJargonBot.Business
{
	partial class TweetFactory
	{
		public class ReplyFactory : TweetFactory
		{
			private Twitter.TweetFinder tweetFinder;

			public ReplyFactory(Twitter.TweetFinder tweetFinder)
			{
				this.tweetFinder = tweetFinder;
			}

			public override String CreateTweet(Generator sentenceGenerator)
			{
				throw new NotImplementedException();
			}
		}
	}
}
