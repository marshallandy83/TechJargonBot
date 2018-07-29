using System;
using System.Collections.Generic;
using TechJargonBot.Business.Data.Tags;

namespace TechJargonBot.Business.Data
{
	internal abstract class Form
	{
		public Form(String value)
		{
			Value = value;
		}

		protected abstract Int32 Index { get; }
		protected abstract String TagWord { get; }
		//public List<String> Words { get; } = new List<String>();
		public String Value { get; }

		public String Tag =>
			String.Concat(
				TagExtractor.TagStartCharacter,
				TagWord,
				TagExtractor.TagEndCharacter);

		public Boolean HasTag(Tag tag)
		{
			return
				String.Compare(
					Tag,
					tag.TagString.Sanitised,
					StringComparison.OrdinalIgnoreCase) == 0;
		}
	}
}