using System;
using System.Collections.ObjectModel;

namespace TechJargonBot.Business.Data
{
	internal abstract class Word
	{
		public ReadOnlyCollection<Form> Forms { get; protected set; }
		public Boolean IsSuitableForHashtag { get; set; }
	}
}