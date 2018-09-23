using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;

namespace TechJargonBot.DebugConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var twitterContext =
				new TwitterContext(
					new SingleUserAuthorizer
					{

						CredentialStore = new InMemoryCredentialStore()
						{
							ConsumerKey = "qNvMFL7wR5JXtiq709xSsHyCV",
							ConsumerSecret = "Khs0eIbeHJdEN8IeA7mqp6g33CLEjRO9soGuDCPjrqPEmwoUc1",
							OAuthToken = "967809339284127744-fr6G3wrRpidi8kM7kV2NXfRD8teWYKO",
							OAuthTokenSecret = "cjH3y4dQSoMiDeTRtB9CIeT4EREendGjr0ZtWUNyIuY8b",
							//ScreenName = twitterAcccountToDisplay,
							//UserID = 66370920
						}
					});

			Console.WriteLine(twitterContext.TweetAsync("test"));
			Console.Read();
		}
	}
}
