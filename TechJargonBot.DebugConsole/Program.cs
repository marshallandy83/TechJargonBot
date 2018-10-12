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
				WriteConversation(twitterContext, replyFactory);

				Console.WriteLine("Press any key...");
				Console.Read();
			}
		}

		private static String InQuotes(String text) => "\"" + text + "\"";

		private async static void WriteConversation(
			TwitterContext twitterContext,
			TweetFactory.ReplyFactory replyFactory)
		{
			Console.Clear();

			for (int i = 0; i < 5; i++)
			{
				Tuple<String, String> reply = replyFactory.GetSentenceAndQuery();
				String sentence = reply.Item1;
				String query = reply.Item2;
				//String sentence = "The problem with hard drives is that the log always needs downloading.";
				//String query =
				//	$@"(" + InQuotes("hard drive") + " OR " + InQuotes("hard drives")
				//	+ ") AND (" + InQuotes("log") + " OR " + InQuotes("logs")
				//	+ ") AND (" + InQuotes("download") + " OR " + InQuotes("downloads") + " OR " + InQuotes("downloading") + " OR " + InQuotes("downloaded") + ")";

				Search searchResponse = await
					twitterContext.Search
					.Where(search =>
						search.Type == SearchType.Search &&
						search.Query == query &&
						search.IncludeEntities == true &&
						search.TweetMode == TweetMode.Extended &&
						search.SearchLanguage == "en-gb")
					.SingleOrDefaultAsync();

				Console.WriteLine("Query: " + query);

				if ((Boolean)(searchResponse?.Statuses != null) && (Boolean)(searchResponse?.Statuses.Any()))
				{
					var status = searchResponse.Statuses.FirstOrDefault();

					Console.WriteLine();
					Console.WriteLine(status.StatusID + ": " + status.FullText);
					Console.WriteLine("> " + sentence);

					return;
				}

				Console.WriteLine("No tweets found.");
			}
		}
	}
}
