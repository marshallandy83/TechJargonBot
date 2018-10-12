using System;
using TechJargonBot.Business.WordSelection;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Business
{
	internal abstract partial class TweetFactory
	{
		public TweetFactory(
			SentenceTemplateType sentenceType,
			WordSelector wordSelector)
		{
			 SentenceGenerator =
				new Generator(
					sentenceProvider: new RandomSentenceTemplateProvider(sentenceType),
					wordSelector: wordSelector,
					stringFormatter: new RegularStringFormatter());
		}

		protected Generator SentenceGenerator { get; }

		public abstract String CreateTweet();
	}
}
