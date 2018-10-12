using System;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Business.WordSelection
{
	partial class RegularWordSelectorTests
	{
		partial class MockWordProvider
		{
			partial class MockWord
			{
				private class MockForm : Form
				{
					public MockForm(String value) : base(value)
					{
					}

					protected override Int32 Index => 0;
					protected override String TagWord => String.Empty;
				}
			}
		}
	}
}
