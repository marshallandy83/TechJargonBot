using System;

namespace TechJargonBot.Vocabulary.Tags
{
	public class Tag : IEquatable<Tag>
	{
		private readonly ITagReplacer _tagReplacer;

		public Tag(
			TagString tagString,
			Func<Word, Boolean> wordSuitabilityPredicate,
			ITagReplacer tagReplacer,
			Boolean isForHashtag,
			Boolean isForMandatoryWord)
			: this(
				  tagString: tagString,
				  identifier: 0,
				  wordSuitabilityPredicate: wordSuitabilityPredicate,
				  tagReplacer: tagReplacer,
				  isForHashtag: isForHashtag,
				  isForMandatoryWord: isForMandatoryWord)
		{
		}

		public Tag(
			TagString tagString,
			Byte identifier,
			Func<Word, Boolean> wordSuitabilityPredicate,
			ITagReplacer tagReplacer,
			Boolean isForHashtag,
			Boolean isForMandatoryWord)
		{
			TagString = tagString;
			Identifier = identifier;
			IsSuitable = wordSuitabilityPredicate;
			IsForHashtag = isForHashtag;
			IsForMandatoryWord = isForMandatoryWord;
			_tagReplacer = tagReplacer;
		}

		public String ReplaceWordInSentenceTemplate(
			String sentenceTemplate,
			String word)
		{
			return _tagReplacer.Replace(sentenceTemplate, word, TagString.Raw);
		}

		public TagString TagString { get; }
		public Boolean HasIdentifer => Identifier != 0;
		public Byte Identifier { get; }
		public Func<Word, Boolean> IsSuitable { get; }
		public Boolean IsForHashtag { get; }
		public Boolean IsForMandatoryWord { get; }

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