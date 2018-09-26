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

			String query = "firewall AND (fix OR fixed OR fixes OR fixing)";
			//String query = @"firewall%20AND%20(fix%20OR%20fixed%20OR%20fixes%20OR%20fixing)";

			var statuses =
				twitterContext
					.Search
					.Where(
						search => search.Type == SearchType.Search
						&&
						search.Query == query
						&&
						search.SearchLanguage == "en-gb"
						&&
						search.IncludeEntities == false)
					.Single()
					.Statuses
					.SelectMany(status => twitterContext.Status.Where(s => s.ID == status.ID))
					.ToList();

			foreach (var status in statuses)
			{
				Console.WriteLine("------------------------------------------------TWEET------------------------------------------------");
				Console.WriteLine(status.Text);
				Console.WriteLine(status.FullText);
			}

			Console.Read();
		}
	}
}
