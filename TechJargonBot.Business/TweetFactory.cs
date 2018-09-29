using System;
using TechJargonBot.Business.Data;

namespace TechJargonBot.Business
{
	internal abstract partial class TweetFactory
	{
		public TweetFactory(SentenceType sentenceType)
		{
			var stringFormatter = new RegularStringFormatter();

			 SentenceGenerator =
				new Generator(
					sentenceProvider: new RandomSentenceProvider(sentenceType),
					wordSelector:
						new RegularWordSelector(
							wordProvider:
								new RandomWordProvider(
									dataProvider:
										new DataProvider(
											dataReader: new CsvFileReader())),
							stringFormatter: stringFormatter),
					stringFormatter: stringFormatter);
		}

		protected Generator SentenceGenerator { get; }

		public abstract String CreateTweet();
	}
}
