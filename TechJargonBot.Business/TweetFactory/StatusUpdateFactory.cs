using System;

namespace TechJargonBot.Business
{
	partial class TweetFactory
	{
		internal class StatusUpdateFactory : TweetFactory
		{
			public override String CreateTweet(Generator sentenceGenerator)
			{
				return sentenceGenerator.Generate();
			}
		}
	}
}
