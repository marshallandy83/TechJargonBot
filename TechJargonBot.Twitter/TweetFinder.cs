using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;

namespace TechJargonBot.Twitter
{
	public class TweetFinder
	{
		private readonly TwitterContext _twitterContext;

		public TweetFinder(TwitterContext twitterContext)
		{
			_twitterContext = twitterContext;
		}

		public async Task<Status> FindTweet(String query)
		{
			Search searchResponse = null;

			try
			{
				searchResponse = await
				_twitterContext.Search
				.Where(search =>
					search.Type == SearchType.Search &&
					search.Query == query &&
					search.IncludeEntities == true &&
					search.TweetMode == TweetMode.Extended &&
					search.SearchLanguage == "en-gb")
				.SingleOrDefaultAsync();
			}
			catch (Exception)
			{
			}

			return
				((Boolean)(searchResponse?.Statuses != null) && (Boolean)(searchResponse?.Statuses.Any()))
				? searchResponse.Statuses.FirstOrDefault()
				: new NoStatus();
		}
	}

	public class NoStatus : Status
	{
	}
}
