using System;
using TechJargonBot.Business.Data;

namespace TechJargonBot.Business
{
	partial class TweetFactory
	{
		internal class StatusUpdateFactory : TweetFactory
		{
			public StatusUpdateFactory() : base(new SentenceType.Status())
			{
			}

			public override String CreateTweet()
			{
				return SentenceGenerator.Generate();
			}
		}
	}
}
