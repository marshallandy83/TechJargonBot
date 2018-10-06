using System;
using System.Collections.ObjectModel;

namespace TechJargonBot.Vocabulary
{
	public partial class Noun : Word
	{
		public Noun(String[] values)
		{
			Forms = new ReadOnlyCollection<Form>(
				new Form[]
				{
					new SingularWithArticle($"{values[0]} {values[1]}"),
					new SingularWithoutArticle(values[1]),
					new Plural(values[2])
				});

			IsSuitableForHashtag = HashtagSuitabilityProvider.IsSuitableForHashtag(values);
		}
	}
}