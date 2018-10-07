using System;
using TechJargonBot.Business.Data;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Business
{
	internal abstract partial class TweetFactory
	{
		public TweetFactory(
			SentenceTemplateType sentenceType,
			Func<Word, Boolean> wordSuitabilityPredicate)
		{
			var stringFormatter = new RegularStringFormatter();

			 SentenceGenerator =
				new Generator(
					sentenceProvider: new RandomSentenceTemplateProvider(sentenceType),
					wordSelector:
						new RegularWordSelector(
							wordProvider:
								new RandomWordProvider(
									dataProvider:
										new DataProvider(
											dataReader: new CsvFileReader()),
									wordSuitabilityPredicate: wordSuitabilityPredicate),
							stringFormatter: stringFormatter),
					stringFormatter: stringFormatter);
		}

		protected Generator SentenceGenerator { get; }

		public abstract String CreateTweet();
	}
}
