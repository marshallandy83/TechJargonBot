using System;

namespace TechJargonBot.Business.Data.Tags
{
	internal interface ITagReplacer
	{
		String Replace(
			String sentence,
			String formattedWord,
			String rawTagString);
	}
}
