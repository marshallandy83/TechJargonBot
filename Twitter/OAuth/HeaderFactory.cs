using System;
using System.Collections.Generic;
using System.Linq;

namespace Twitter.OAuth
{
	public static class HeaderFactory
	{
		/// <summary>
		/// Generate the raw OAuth HTML header from the values (including signature).
		/// </summary>
		public static String Create(Dictionary<String, String> data)
		{
			return
				"OAuth "
				+
				String.Join(
				", ",
				data
					.Where(kvp => kvp.Key.StartsWith("oauth_"))
					.Select(kvp => String.Format("{0}=\"{1}\"", Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value)))
					.OrderBy(s => s));
		}
	}
}
