using System;
using TechJargonBot.Business.Data;
using TechJargonBot.Business.WordSelection;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Business
{
	partial class TweetFactory
	{
		public class ReplyFactory : TweetFactory
		{
			private readonly Twitter.IQueryFactory _queryFactory;
			private readonly Twitter.TweetFinder _tweetFinder;

			public ReplyFactory(
				Twitter.IQueryFactory queryFactory,
				Twitter.TweetFinder tweetFinder)
				: base(
					sentenceType: new SentenceTemplateType.Reply(),
					wordSelector:
						new DomainSpecificWordSelector(
							wordProvider:
								new RandomWordProvider(
									dataProvider:
										new DataProvider(
											dataReader: new CsvFileReader()),
									wordSuitabilityPredicate: word => true),
							stringFormatter: new RegularStringFormatter()))
			{
				_queryFactory = queryFactory;
				_tweetFinder = tweetFinder;
			}

			public Tuple<String, String> GetSentenceAndQuery()
			{
				Sentence sentence = SentenceGenerator.Generate();

				String query = _queryFactory.Create(sentence.AllWords);

				return new Tuple<String, String>(sentence.Text, query);
			}

			public override String CreateTweet()
			{
				Sentence sentence = SentenceGenerator.Generate();

				String query = _queryFactory.Create(sentence.AllWords);

				return sentence.Text;
			}
		}
	}
}
