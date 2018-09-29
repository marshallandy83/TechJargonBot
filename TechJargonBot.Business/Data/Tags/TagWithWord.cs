using System;
using System.Linq;

namespace TechJargonBot.Business.Data.Tags
{
	internal class TagWithWord : ITagWithWord
	{
		private readonly IStringFormatter _stringFormatter;

		public TagWithWord(
			IStringFormatter stringFormatter,
			Tag tag,
			Word word)
		{
			_stringFormatter = stringFormatter;
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
				_stringFormatter.FormatString(
					ReplacedWord,
					Tag.TagString);

			return
				Tag.ReplaceWordInSentenceTemplate(
					sentence,
					formattedWord);
		}
	}
}