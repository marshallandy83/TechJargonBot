using System;

namespace TechJargonBot.Vocabulary
{
	partial class Verb
	{
		internal class Root : Form
		{
			public Root(String value) : base(value)
			{
			}

			protected override Int32 Index => 0;
			protected override String TagWord => "do";
		}
	}
}
