﻿using System;

namespace TechJargonBot.Vocabulary
{
	partial class Adjective
	{
		internal class WithoutArticle : Form
		{
			public WithoutArticle(String value) : base(value)
			{
			}

			protected override Int32 Index => 0;
			protected override String TagWord => "tech";
		}
	}
}