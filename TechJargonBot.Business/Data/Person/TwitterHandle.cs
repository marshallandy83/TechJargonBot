using System;

namespace TechJargonBot.Business.Data
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