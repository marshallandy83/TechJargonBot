using System;
using TechJargonBot.Vocabulary;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Business
{
	public partial class RegularWordSelectorTests
	{
		private partial class MockWordProvider : IWordProvider
		{
			private Byte _wordNumber = 0;

			public Word GetNewWord(Tag tag)
			{
				_wordNumber++;

				return
					new MockWord(
						String.Concat("Word ", _wordNumber));
			}
		}
	}
}
