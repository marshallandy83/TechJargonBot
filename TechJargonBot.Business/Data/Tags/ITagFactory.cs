using System;

namespace TechJargonBot.Business.Data.Tags
{
	internal interface ITagFactory
	{
		Tag CreateTag(String value);
	}
}