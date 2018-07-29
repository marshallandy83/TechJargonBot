using System;
using System.Collections.Generic;
using TechJargonBot.Business.Data.Tags;

namespace TechJargonBot.Business
{
	internal interface IWordSelector
	{
		IEnumerable<TagWithWord> CreateTagsWithWords(IEnumerable<Tag> tags);
	}
}