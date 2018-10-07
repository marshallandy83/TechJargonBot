using System;
using System.Collections.ObjectModel;

namespace TechJargonBot.Vocabulary
{
	public abstract class Word
	{
		public ReadOnlyCollection<Form> Forms { get; protected set; }
		public Boolean IsSuitableForHashtag { get; set; }
		public Boolean IsDomainSpecific { get; set; }
	}
}