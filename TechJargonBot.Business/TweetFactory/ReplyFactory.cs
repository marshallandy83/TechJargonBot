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
				: base(new SentenceTemplateType.Reply())
			{
				_queryFactory = queryFactory;
				_tweetFinder = tweetFinder;
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
