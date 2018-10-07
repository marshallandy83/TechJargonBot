using System;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Business
{
	partial class TweetFactory
	{
		internal class StatusUpdateFactory : TweetFactory
		{
			public StatusUpdateFactory()
				: base(
					  sentenceType: new SentenceTemplateType.Status(),
					  wordSuitabilityPredicate: word => true)
			{
			}

			public override String CreateTweet()
			{
				return SentenceGenerator.Generate().Text;
			}
		}
	}
}
