using System;
using System.Linq;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Business
{
	internal class RegularStringFormatter : IStringFormatter
	{
		public String FormatString(String word, TagString tagString)
		{
			if (tagString.IsAllUppercase)
				word = word.ToUpper();
			else if (tagString.IsFirstLetterUppercase)
				word = CapitaliseFirstLetter(word);

			if (tagString.IsHashTag)
				word = FormatHashtag(word);

			return word;
		}

		private String FormatHashtag(String word)
		{
			return
				String.Concat(
					'#',
					String.Concat(
						values:
							word
							.Split(' ')
							.Select(w => CapitaliseFirstLetter(w))));
		}

		private String CapitaliseFirstLetter(String word)
		{
			return
				String.Concat(
					Char.ToUpper(word[0]),
					word.Substring(1));
		}
	}
}
