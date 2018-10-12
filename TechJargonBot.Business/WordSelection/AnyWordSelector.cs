using System.Collections.Generic;
using System.Linq;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Business.WordSelection
{
	class AnyWordSelector : WordSelector
	{
		public AnyWordSelector(
			IWordProvider wordProvider,
			IStringFormatter stringFormatter)
			: base(wordProvider, stringFormatter)
		{
		}

		public override IEnumerable<TagWithWord> CreateTagsWithWords(IEnumerable<Tag> tags) =>
			tags.Select(tag => CreateTagWithWord(tag, base.WordProvider));
	}
}
