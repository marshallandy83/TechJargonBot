using System;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Business
{
	internal interface IStringFormatter
	{
		String FormatString(String word, TagString tagString);
	}
}