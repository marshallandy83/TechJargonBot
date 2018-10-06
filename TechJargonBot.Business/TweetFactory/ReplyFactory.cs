using System;
using System.Linq;
using TechJargonBot.Business.Data;

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

				String query =
					_queryFactory.Create(
						sentence.TagsWithWords.SelectMany(
							tagWithWord => tagWithWord.Word.Forms.Select(
								form => form.Value)));

				return sentence.Text;
			}
		}
	}
}
