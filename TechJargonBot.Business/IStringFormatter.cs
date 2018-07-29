using System;
using TechJargonBot.Business.Data.Tags;

namespace TechJargonBot.Business
{
	internal interface IStringFormatter
	{
		String FormatString(String word, TagString tagString);
	}
}