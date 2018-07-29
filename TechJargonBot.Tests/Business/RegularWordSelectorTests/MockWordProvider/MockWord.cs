using System;
using System.Collections.ObjectModel;
using TechJargonBot.Business.Data;

namespace TechJargonBot.Business
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
