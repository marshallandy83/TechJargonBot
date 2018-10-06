using System;

namespace TechJargonBot.Vocabulary.Tags
{
	public interface ITagReplacer
	{
		String Replace(
			String sentenceTemplate,
			String formattedWord,
			String rawTagString);
	}
}
