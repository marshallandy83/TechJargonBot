using System;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Vocabulary
{
	public abstract class Form
	{
		public Form(String value)
		{
			Value = value;
		}

		protected abstract Int32 Index { get; }
		protected abstract String TagWord { get; }
		public String Value { get; }

		public String Tag =>
			String.Concat(
				TagExtractor.TagStartCharacter,
				TagWord,
				TagExtractor.TagEndCharacter);

		public String ValueInQuotes => "\"" + Value + "\"";

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