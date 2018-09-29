using System;
using TechJargonBot.Business.Data;

namespace TechJargonBot.Business
{
	internal abstract partial class TweetFactory
	{
		public TweetFactory(SentenceType sentenceType)
		{
			var randomNumberGenerator = new Random();
			var stringFormatter = new RegularStringFormatter();

			 SentenceGenerator =
				new Generator(
					sentenceProvider: new RandomSentenceProvider(randomNumberGenerator, sentenceType),
					wordSelector:
						new RegularWordSelector(
							wordProvider:
								new RandomWordProvider(
									dataProvider: new DataProvider(dataReader: new CsvFileReader()),
									randomNumberGenerator: randomNumberGenerator),
							stringFormatter: stringFormatter),
					stringFormatter: stringFormatter);
		}

		protected Generator SentenceGenerator { get; }

		public abstract String CreateTweet();
	}
}
