using System;

namespace TechJargonBot.Vocabulary
{
	partial class Adjective
	{
		internal class WithArticle : Form
		{
			public WithArticle(String value) : base(value)
			{
			}

			protected override Int32 Index => 0;
			protected override String TagWord => "a tech";
		}
	}
}