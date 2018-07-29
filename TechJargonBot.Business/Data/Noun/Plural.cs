﻿using System;

namespace TechJargonBot.Business.Data
{
	partial class Noun
	{
		internal class Plural : Form
		{
			public Plural(String value) : base(value)
			{
			}

			protected override Int32 Index => 2;
			protected override String TagWord => "things";
		}
	}
}