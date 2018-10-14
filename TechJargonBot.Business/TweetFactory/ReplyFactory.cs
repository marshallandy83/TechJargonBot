using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LinqToTwitter;
using TechJargonBot.Business.Data;
using TechJargonBot.Business.WordSelection;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Business
{
	partial class TweetFactory
	{
		public class ReplyFactory : TweetFactory
		{
			private const Int32 _numberOfQueryRetries = 10;

			private readonly Twitter.IQueryFactory _queryFactory;
			private readonly Twitter.TweetFinder _tweetFinder;

			public ReplyFactory(
				TwitterContext twitterContext,
				Twitter.IQueryFactory queryFactory,
				Twitter.TweetFinder tweetFinder)
				: base(
					twitterContext: twitterContext,
					sentenceType: new SentenceTemplateType.Reply(),
					wordSelectorFactory: new DomainSpecificWordSelectorFactory())
			{
				_queryFactory = queryFactory;
				_tweetFinder = tweetFinder;
			}

			public Tuple<String, String> GetSentenceAndQuery()
			{
				Sentence sentence = SentenceGenerator.Generate();

				String query =
					_queryFactory.Create(
						PickWordsForQuery(sentence));

				return new Tuple<String, String>(sentence.Text, query);
			}

			private IEnumerable<Word> PickWordsForQuery(Sentence sentence)
			{
				return
					sentence
					.AllMandatoryWords
					.Concat(
						sentence
						.AllNonMandatoryWords
						.PickSomeAtRandom());
			}

			public override String SendTweet()
			{
				for (int i = 0; i < _numberOfQueryRetries; i++)
				{
					Sentence sentence = SentenceGenerator.Generate();

					String query =
						_queryFactory.Create(
							PickWordsForQuery(sentence));

					Status tweet = _tweetFinder.FindTweet(query).Result;

					String summary;

					if (tweet.GetType() != typeof(Twitter.NoStatus))
					{
						PostReply(tweet.StatusID, sentence.Text);
						summary = tweet.FullText ?? tweet.Text;

						return
							summary +
							Environment.NewLine + Environment.NewLine +
							$"> {sentence.Text}" +
							Environment.NewLine + Environment.NewLine +
							$"QUERY: {query}" +
							Environment.NewLine + Environment.NewLine;
					}

					Thread.Sleep(3000);
				}

				return "No tweet found";
			}

			private async void PostReply(
				UInt64 tweetId,
				String sentence)
			{
				try
				{
					await TwitterContext.ReplyAsync(tweetId, sentence, autoPopulateReplyMetadata: true);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
					throw;
				}
			}
		}
	}
}
