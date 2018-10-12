using System;
using System.Text.RegularExpressions;

namespace TechJargonBot.Vocabulary.Tags
{
	public class TagFactory : ITagFactory
	{
		private Boolean _tagIsForHashtag;
		private Boolean _tagIsForMandatoryWord;
		private Func<Word, Boolean> _wordSuitabilityPredicate;
		private String _rawTagString;
		private String _sanitisedTagString;

		public Tag CreateTag(String tagString)
		{
			_rawTagString = tagString;
			_sanitisedTagString = tagString;

			ProcessHashtag(tagString);
			ProcessMandate(tagString);

			var tagIdentifierString = Regex.Match(tagString, @"([\d])");

			Byte tagIdentifier =
				tagIdentifierString.Success
				? SanitiseTagStringAndGetIdentifier(tagIdentifierString)
				: (Byte)0;

			return
				new Tag(
					new TagString(
						_rawTagString,
						_sanitisedTagString),
					tagIdentifier,
					_wordSuitabilityPredicate,
					new TagReplacer(),
					isForHashtag: _tagIsForHashtag,
					isForMandatoryWord: _tagIsForMandatoryWord);
		}

		private void ProcessHashtag(String tagString)
		{
			if (TagIsForHashtag(tagString))
			{
				_tagIsForHashtag = true;
				_sanitisedTagString = tagString.Remove(tagString.IndexOf('#'), 1);
				_wordSuitabilityPredicate = new Func<Word, Boolean>(word => word.IsSuitableForHashtag);
			}
			else
			{
				_tagIsForHashtag = false;
				_wordSuitabilityPredicate = new Func<Word, Boolean>(word => true);
			}
		}

		private void ProcessMandate(String tagString)
		{
			if (TagIsForMandatoryWord(tagString))
			{
				_tagIsForMandatoryWord = true;
				_sanitisedTagString = tagString.Remove(tagString.IndexOf('*'), 1);
			}
			else
			{
				_tagIsForMandatoryWord = false;
			}
		}

		private Boolean TagIsForHashtag(String tagString) => tagString[1] == '#';
		private Boolean TagIsForMandatoryWord(String tagString) => tagString[tagString.Length - 2] == '*';

		private Byte SanitiseTagStringAndGetIdentifier(Match tagIdentifier)
		{
			String tagIdentifierString = tagIdentifier.Groups[1].Value;

			_sanitisedTagString =
				_sanitisedTagString.Remove(
					_sanitisedTagString.IndexOf(tagIdentifierString),
					tagIdentifierString.Length);

			return Byte.Parse(tagIdentifierString);
		}
	}
}