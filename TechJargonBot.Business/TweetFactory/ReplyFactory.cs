using System;

namespace TechJargonBot.Business
{
	partial class TweetFactory
	{
		public class ReplyFactory : TweetFactory
		{
			public override String CreateTweet(Generator sentenceGenerator)
			{
				throw new NotImplementedException();
			}
		}
	}
}
