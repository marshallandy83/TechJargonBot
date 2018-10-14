using System;
using LinqToTwitter;
using TechJargonBot.Business.WordSelection;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Business
{
	internal abstract partial class TweetFactory
	{
		public TweetFactory(
			TwitterContext twitterContext,
			SentenceTemplateType sentenceType,
			IWordSelectorFactory wordSelectorFactory)
		{
			TwitterContext = twitterContext;

			 SentenceGenerator =
				new Generator(
					sentenceProvider: new RandomSentenceTemplateProvider(sentenceType),
					wordSelectorFactory: wordSelectorFactory,
					stringFormatter: new RegularStringFormatter());
		}

		protected TwitterContext TwitterContext { get; }
		protected Generator SentenceGenerator { get; }

		public abstract String SendTweet();
	}
}
