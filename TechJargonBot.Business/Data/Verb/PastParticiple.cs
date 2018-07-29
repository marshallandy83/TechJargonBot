using System;

namespace TechJargonBot.Business.Data
{
	partial class Verb
	{
		internal class PastParticiple : Form
		{
			public PastParticiple(String value) : base(value)
			{
			}

			protected override Int32 Index => 2;
			protected override String TagWord => "done";
		}
	}
}
