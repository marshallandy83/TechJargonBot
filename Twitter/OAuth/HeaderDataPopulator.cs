using System;
using System.Collections.Generic;

namespace Twitter.OAuth
{
	public class HeaderDataPopulator
	{
		private readonly String _consumerKey;
		private readonly String _accessToken;
		private readonly DateTime epochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public HeaderDataPopulator(String consumerKey, String accessToken)
		{
			_consumerKey = consumerKey;
			_accessToken = accessToken;
		}

		public Dictionary<String, String> Populate(Dictionary<String, String> headerData)
		{
			// Timestamps are in seconds since 1/1/1970.
			Int32 timestamp = (Int32)((DateTime.UtcNow - epochUtc).TotalSeconds);

			// Add all the OAuth headers we'll need to use when constructing the hash.
			headerData.Add("oauth_consumer_key", _consumerKey);
			headerData.Add("oauth_signature_method", "HMAC-SHA1");
			headerData.Add("oauth_timestamp", timestamp.ToString());
			headerData.Add("oauth_nonce", "a"); // Required, but Twitter doesn't appear to use it, so "a" will do.
			headerData.Add("oauth_token", _accessToken);
			headerData.Add("oauth_version", "1.0");

			return headerData;
		}
	}
}