using System;

namespace TechJargonBot.Business.Data
{
	partial class Noun
	{
		internal class SingularWithoutArticle : Form
		{
			public SingularWithoutArticle(String value) : base(value)
			{
			}

			protected override Int32 Index => 1;
			protected override String TagWord => "thing";
		}
	}
}