using System;
using LinqToTwitter;
using TechJargonBot.Business.Data;
using TechJargonBot.Business.WordSelection;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Business
{
	partial class TweetFactory
	{
		internal class StatusUpdateFactory : TweetFactory
		{
			public StatusUpdateFactory(TwitterContext twitterContext)
				: base(
					twitterContext: twitterContext,
					sentenceType: new SentenceTemplateType.Status(),
					wordSelectorFactory: new AnyWordSelectorFactory())
			{
			}

			public override String SendTweet()
			{
				String statusUpdate = SentenceGenerator.Generate("Everybody talks about [doing1] the [things] but nobody ever mentions [doing1] the [things].").Text;

				PostStatusUpdate(statusUpdate);

				return statusUpdate;
			}

			private async void PostStatusUpdate(String sentence)
			{
				await TwitterContext.TweetAsync(sentence);
			}
		}
	}
}
