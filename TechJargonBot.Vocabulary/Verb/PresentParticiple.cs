﻿using System;

namespace TechJargonBot.Vocabulary
{
	partial class Verb
	{
		internal class PresentParticiple : Form
		{
			public PresentParticiple(String value) : base(value)
			{
			}

			protected override Int32 Index => 1;
			protected override String TagWord => "doing";
		}
	}
}
