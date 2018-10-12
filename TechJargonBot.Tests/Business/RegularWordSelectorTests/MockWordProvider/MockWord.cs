using System;
using System.Collections.ObjectModel;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Business.WordSelection
{
	partial class RegularWordSelectorTests
	{
		partial class MockWordProvider
		{
			private partial class MockWord : Word
			{
				public MockWord(String value)
				{
					Forms = new ReadOnlyCollection<Form>(
						new Form[]
						{
							new MockForm(value)
						});
				}
			}
		}
	}
}
