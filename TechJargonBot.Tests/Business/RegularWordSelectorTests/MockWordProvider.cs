using System;
using TechJargonBot.Business.Data;
using TechJargonBot.Business.Data.Tags;

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
