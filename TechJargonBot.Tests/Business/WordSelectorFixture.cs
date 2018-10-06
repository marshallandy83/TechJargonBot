using Moq;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Business
{
	public class WordSelectorFixture
	{
		public WordSelectorFixture()
		{
			MockTagReplacer = new Mock<ITagReplacer>().Object;
		}

		internal ITagReplacer MockTagReplacer { get; }
	}
}