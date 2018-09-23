using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Twitter
{
	/// <summary>
	/// Simple class for sending tweets to Twitter using Single-user OAuth.
	/// https://dev.twitter.com/oauth/overview/single-user
	/// 
	/// Get your access keys by creating an app at apps.twitter.com then visiting the
	/// "Keys and Access Tokens" section for your app. They can be found under the
	/// "Your Access Token" heading.
	/// </summary>
	public class TwitterApi
	{
		const string TwitterApiBaseUrl = "https://api.twitter.com/1.1/";
		readonly string consumerKey, consumerKeySecret, accessToken, accessTokenSecret;
		readonly HMACSHA1 sigHasher;
		private readonly OAuth.HeaderDataPopulator _headerDataPopulator;

		/// <summary>
		/// Creates an object for sending tweets to Twitter using Single-user OAuth.
		/// 
		/// Get your access keys by creating an app at apps.twitter.com then visiting the
		/// "Keys and Access Tokens" section for your app. They can be found under the
		/// "Your Access Token" heading.
		/// </summary>
		public TwitterApi(string consumerKey, string consumerKeySecret, string accessToken, string accessTokenSecret)
		{
			this.consumerKey = consumerKey;
			this.consumerKeySecret = consumerKeySecret;
			this.accessToken = accessToken;
			this.accessTokenSecret = accessTokenSecret;

			_headerDataPopulator = new OAuth.HeaderDataPopulator(consumerKey, accessToken);

			sigHasher =
				new HMACSHA1(
					new ASCIIEncoding().GetBytes(String.Format($"{consumerKeySecret}&{accessTokenSecret}")));
		}

		/// <summary>
		/// Sends a tweet with the supplied text and returns the response from the Twitter API.
		/// </summary>
		public Task<String> Tweet(String text)
		{
			var data = new Dictionary<String, String>
			{
				{ "status", text },
				{ "trim_user", "1" }
			};

			return SendRequest("statuses/update.json", data);
		}

		Task<String> SendRequest(String url, Dictionary<String, String> data)
		{
			String fullUrl = TwitterApiBaseUrl + url;

			data = _headerDataPopulator.Populate(data);

			// Generate the OAuth signature and add it to our payload.
			data.Add("oauth_signature", GenerateSignature(fullUrl, data));

			// Build the OAuth HTTP Header from the data.
			string oAuthHeader = OAuth.HeaderFactory.Create(data);

			// Build the form data (exclude OAuth stuff that's already in the header).
			var formData = new FormUrlEncodedContent(data.Where(kvp => !kvp.Key.StartsWith("oauth_")));

			return SendRequest(fullUrl, oAuthHeader, formData);
		}

		/// <summary>
		/// Generate an OAuth signature from OAuth header values.
		/// </summary>
		string GenerateSignature(string url, Dictionary<string, string> data)
		{
			var sigString = string.Join(
				"&",
				data
					.Union(data)
					.Select(kvp => string.Format("{0}={1}", Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value)))
					.OrderBy(s => s)
			);

			var fullSigData = string.Format(
				"{0}&{1}&{2}",
				"POST",
				Uri.EscapeDataString(url),
				Uri.EscapeDataString(sigString.ToString())
			);

			return Convert.ToBase64String(sigHasher.ComputeHash(new ASCIIEncoding().GetBytes(fullSigData.ToString())));
		}

		/// <summary>
		/// Send HTTP Request and return the response.
		/// </summary>
		async Task<string> SendRequest(string fullUrl, string oAuthHeader, FormUrlEncodedContent formData)
		{
			using (var http = new HttpClient())
			{
				http.DefaultRequestHeaders.Add("Authorization", oAuthHeader);

				var httpResp = await http.PostAsync(fullUrl, formData);
				var respBody = await httpResp.Content.ReadAsStringAsync();

				return respBody;
			}
		}
	}
}
