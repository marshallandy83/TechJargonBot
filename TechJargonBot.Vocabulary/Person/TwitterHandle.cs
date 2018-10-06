using System;

namespace TechJargonBot.Vocabulary
{
	partial class Person
	{
		internal class TwitterHandle : Form
		{
			public TwitterHandle(String value) : base(value)
			{
			}

			protected override Int32 Index => 0;
			protected override String TagWord => "TwitterHandle";
		}
	}
}