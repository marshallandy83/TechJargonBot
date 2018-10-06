using System;

namespace TechJargonBot.Vocabulary
{
	partial class Verb
	{
		internal class ThirdPersonSingular : Form
		{
			public ThirdPersonSingular(String value) : base(value)
			{
			}

			protected override Int32 Index => 3;
			protected override String TagWord => "does";
		}
	}
}
