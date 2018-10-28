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
				String statusUpdate = SentenceGenerator.Generate().Text;

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
