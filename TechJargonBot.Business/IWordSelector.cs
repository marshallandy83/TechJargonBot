using System.Collections.Generic;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Business
{
	internal interface IWordSelector
	{
		IEnumerable<TagWithWord> CreateTagsWithWords(IEnumerable<Tag> tags);
	}
}