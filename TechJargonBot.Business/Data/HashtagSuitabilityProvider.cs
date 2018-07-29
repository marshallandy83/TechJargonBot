using System;
using System.Linq;

namespace TechJargonBot.Business.Data
{
	internal class HashtagSuitabilityProvider
	{
		public static Boolean IsSuitableForHashtag(String[] values)
		{
			return
				values.All(IsSuitableForHashtag);
		}

		private static Boolean IsSuitableForHashtag(String word)
		{
			return
				Char.IsLetter(word[0])
				&&
				word.All(IsSuitableForHashtag);
		}

		private static Boolean IsSuitableForHashtag(Char character)
		{
			return
				Char.IsLetterOrDigit(character)
				||
				Char.IsWhiteSpace(character);
		}
	}
}
