using System;
using System.Collections.ObjectModel;

namespace TechJargonBot.Vocabulary
{
	public partial class Person : Word
	{
		public Person(String[] values)
		{
			Forms = new ReadOnlyCollection<Form>(
				new Form[]
				{
					new TwitterHandle(values[0])
				});

			IsDomainSpecific = true;
		}
	}
}