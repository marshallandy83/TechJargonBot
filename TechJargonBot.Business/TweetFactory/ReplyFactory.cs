using System;
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
					  wordSuitabilityPredicate: word => word.IsDomainSpecific)
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
