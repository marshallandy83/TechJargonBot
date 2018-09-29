using System;

namespace TechJargonBot.Business.Data.Tags
{
	internal interface ITagReplacer
	{
		String Replace(
			String sentenceTemplate,
			String formattedWord,
			String rawTagString);
	}
}
