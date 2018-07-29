using System;
using System.Text.RegularExpressions;

namespace TechJargonBot.Business.Data.Tags
{
	internal class TagReplacer : ITagReplacer
	{
		public String Replace(
			String sentence,
			String formattedWord,
			String rawTagString)
		{
			var regex = new Regex(Regex.Escape(rawTagString));

			return
				regex.Replace(
					input: sentence,
					replacement: formattedWord,
					count: 1);
		}
	}
}