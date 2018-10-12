using System;
using TechJargonBot.Business.Data;
using TechJargonBot.Business.WordSelection;
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
					wordSelector:
						new AnyWordSelector(
							wordProvider:
								new RandomWordProvider(
									dataProvider:
										new DataProvider(
											dataReader: new CsvFileReader()),
									wordSuitabilityPredicate: word => true),
							stringFormatter: new RegularStringFormatter()))
			{
			}

			public override String CreateTweet()
			{
				return SentenceGenerator.Generate().Text;
			}
		}
	}
}
