using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;
using TechJargonBot.Business;

namespace TechJargonBot.DebugConsole
{
	class Program
	{
		static void Main(String[] args)
		{
			var twitterContext =
				new TwitterContext(
					new SingleUserAuthorizer
					{
						CredentialStore = new InMemoryCredentialStore()
						{
							ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"],
							ConsumerSecret = ConfigurationManager.AppSettings["ConsumerKeySecret"],
							OAuthToken = ConfigurationManager.AppSettings["AccessToken"],
							OAuthTokenSecret = ConfigurationManager.AppSettings["AccessTokenSecret"],
						}
					});

			var replyFactory = new TweetFactory.ReplyFactory(
				new Twitter.AllWordsQueryFactory(),
				new Twitter.TweetFinder());

			while (true)
			{
				//var result = replyFactory.QueryAndSentence();

				//Status status = null;

				//var statuses =
				//	await
				//	twitterContext
				//		.Search
				//		.Where(
				//			search => search.Type == SearchType.Search
				//			&&
				//			//search.Query == "(invalidate OR invalidating OR invalidated OR invalidates) AND (a CTE OR CTE OR CTEs)")
				//			search.Query == "invalidate AND CTE")
				//			//&&
				//			//search.SearchLanguage == "en-gb"
				//			//&&
				//			//search.TweetMode == TweetMode.Extended)
				//		.Single()
				//		.Statuses;
				//.FirstOrDefault();
				//.ToList();

				//Console.WriteLine(result.Item1);
				//Console.WriteLine(result.Item2);

				//(invalidate OR invalidating OR invalidated OR invalidates) AND(a CTE OR CTE OR CTEs)

				//foreach (var status in statuses)
				//{
				//if (status != null)
				//{
				//	//Console.WriteLine("------------------------------------------------TWEET------------------------------------------------");
				//	//Console.WriteLine(status.StatusID);
				//	//Console.WriteLine(status.FullText);
				//	//Console.WriteLine(status.FullText);

				//}
				//}
				//else
				//{
				//	Console.WriteLine("No tweets found");
				//}

				GetTweets(twitterContext);

				Console.Read();
			}
		}

		private async static void GetTweets(TwitterContext twitterContext)
		{
			string searchTerm = "invalidate AND CTE";

			Search searchResponse =
				await
				(from search in twitterContext.Search
				 where search.Type == SearchType.Search &&
					   search.Query == searchTerm &&
					   search.IncludeEntities == true &&
					   search.TweetMode == TweetMode.Extended
				 select search)
				.SingleOrDefaultAsync();

			if (searchResponse?.Statuses != null)
				searchResponse.Statuses.ForEach(tweet =>
					Console.WriteLine(
						"\n  User: {0} ({1})\n  Tweet: {2}",
						tweet.User.ScreenNameResponse,
						tweet.User.UserIDResponse,
						tweet.Text ?? tweet.FullText));
			else
				Console.WriteLine("No entries found.");
		}
	}
}
