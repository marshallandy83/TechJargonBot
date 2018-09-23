using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;

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

			Console.WriteLine(twitterContext.TweetAsync("test").Status);
			Console.Read();
		}
	}
}
