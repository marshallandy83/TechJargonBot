using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Business.WordSelection
{
	partial class DomainSpecificWordSelector
	{
		private class TagWithWordProvider
		{
			public TagWithWordProvider(
				Tag tag,
				IWordProvider wordProvider)
			{
				Tag = tag;
				WordProvider = wordProvider;
			}

			public Tag Tag { get; }
			public IWordProvider WordProvider { get; }
		}
	}
}
