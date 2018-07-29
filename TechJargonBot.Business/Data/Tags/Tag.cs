using System;

namespace TechJargonBot.Business.Data.Tags
{
	internal class Tag : IEquatable<Tag>
	{
		private readonly ITagReplacer _tagReplacer;

		public Tag(
			TagString tagString,
			Func<Word, Boolean> wordSuitabilityPredicate,
			ITagReplacer tagReplacer,
			Boolean isForHashtag)
			: this(
				  tagString: tagString,
				  identifier: 0,
				  wordSuitabilityPredicate: wordSuitabilityPredicate,
				  tagReplacer: tagReplacer,
				  isForHashtag: isForHashtag)
		{
		}

		public Tag(
			TagString tagString,
			Byte identifier,
			Func<Word, Boolean> wordSuitabilityPredicate,
			ITagReplacer tagReplacer,
			Boolean isForHashtag)
		{
			TagString = tagString;
			Identifier = identifier;
			IsSuitable = wordSuitabilityPredicate;
			IsForHashtag = isForHashtag;
			_tagReplacer = tagReplacer;
		}

		public String ReplaceWordInSentence(
			String sentence,
			String word)
		{
			return _tagReplacer.Replace(sentence, word, TagString.Raw);
		}

		public TagString TagString { get; }
		public Boolean HasIdentifer => Identifier != 0;
		public Byte Identifier { get; }
		public Func<Word, Boolean> IsSuitable { get; }
		public Boolean IsForHashtag { get; }

		public bool Equals(Tag other)
		{
			return
				other != null
				&&
				other.TagString == TagString
				&&
				other.Identifier == Identifier;
		}
	}
}