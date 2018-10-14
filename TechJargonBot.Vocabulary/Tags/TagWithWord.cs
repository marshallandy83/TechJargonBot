using System;
using System.Linq;

namespace TechJargonBot.Vocabulary.Tags
{
	public class TagWithWord : ITagWithWord
	{
		private readonly Func<String, TagString, String> _formatString;

		public TagWithWord(
			Func<String, TagString, String> formatString,
			Tag tag,
			Word word)
		{
			_formatString = formatString;
			Tag = tag;
			Word = word;
		}

		public Tag Tag { get; }
		public Word Word { get; }

		public String ReplacedWord
		{
			get
			{
				return
					Word.Forms.Single(form => form.HasTag(Tag)).Value;
			}
		}

		public String ReplaceWordInSentence(String sentence)
		{
			String formattedWord =
				_formatString(
					ReplacedWord,
					Tag.TagString);

			return
				Tag.ReplaceWordInSentenceTemplate(
					sentence,
					formattedWord);
		}
	}
}