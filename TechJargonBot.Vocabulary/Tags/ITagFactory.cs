using System;

namespace TechJargonBot.Vocabulary.Tags
{
	public interface ITagFactory
	{
		Tag CreateTag(String value);
	}
}