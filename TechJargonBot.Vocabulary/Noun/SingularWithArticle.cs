﻿using System;

namespace TechJargonBot.Vocabulary
{
	partial class Noun
	{
		internal class SingularWithArticle : Form
		{
			public SingularWithArticle(String value) : base(value)
			{
			}

			protected override Int32 Index => 0;
			protected override String TagWord => "a thing";
		}
	}
}