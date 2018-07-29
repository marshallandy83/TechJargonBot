using System;
using System.Collections.ObjectModel;

namespace TechJargonBot.Business.Data
{
	internal partial class Verb : Word
	{
		public Verb(String[] values)
		{
			Forms = new ReadOnlyCollection<Form>(
				new Form[]
				{
					new Root(values[0]),
					new PresentParticiple(values[1]),
					new PastParticiple(values[2]),
					new ThirdPersonSingular(values[3])
				});

			IsSuitableForHashtag = HashtagSuitabilityProvider.IsSuitableForHashtag(values);
		}
	}
}