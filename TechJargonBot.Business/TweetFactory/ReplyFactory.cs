using System;
using TechJargonBot.Business.Data.Tags;

namespace TechJargonBot.Business
{
	partial class TweetFactory
	{
		public class ReplyFactory : TweetFactory
		{
			private readonly Twitter.TweetFinder _tweetFinder;
			private readonly IWordProvider _wordProvider;
			private readonly TagExtractor _tagExtractor;

			public ReplyFactory(
				Twitter.TweetFinder tweetFinder,
				IWordProvider wordProvider,
				TagExtractor tagExtractor)
			{
				_tweetFinder = tweetFinder;
				_wordProvider = wordProvider;
				_tagExtractor = tagExtractor;
			}

			public override String CreateTweet(Generator sentenceGenerator)
			{
				return String.Empty;
			}
		}
	}
}
