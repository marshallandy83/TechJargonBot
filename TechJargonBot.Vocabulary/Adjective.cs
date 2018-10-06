using System;
using System.Collections.ObjectModel;

namespace TechJargonBot.Vocabulary
{
	public partial class Adjective : Word
	{
		public Adjective(String[] values)
		{
			Forms = new ReadOnlyCollection<Form>(
				new Form[]
				{
					new WithArticle($"{values[0]} {values[1]}"),
					new WithoutArticle(values[1])
				});

			IsSuitableForHashtag = HashtagSuitabilityProvider.IsSuitableForHashtag(values);
		}
	}
}