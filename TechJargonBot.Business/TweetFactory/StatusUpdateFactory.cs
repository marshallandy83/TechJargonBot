using System;
using TechJargonBot.Business.Data;

namespace TechJargonBot.Business
{
	partial class TweetFactory
	{
		internal class StatusUpdateFactory : TweetFactory
		{
			public StatusUpdateFactory()
				: base(new SentenceTemplateType.Status())
			{
			}

			public override String CreateTweet()
			{
				return SentenceGenerator.Generate().Text;
			}
		}
	}
}
