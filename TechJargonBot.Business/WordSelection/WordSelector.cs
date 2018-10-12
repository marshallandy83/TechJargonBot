using System.Collections.Generic;
using System.Linq;
using TechJargonBot.Vocabulary;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Business.WordSelection
{
	internal abstract class WordSelector
	{
		private readonly IStringFormatter _stringFormatter;
		private readonly IWordProvider _wordProvider;
		private List<TagWithWord> _tagsWithWordsToReuse = new List<TagWithWord>();

		public WordSelector(
			IWordProvider wordProvider,
			IStringFormatter stringFormatter)
		{
			WordProvider = wordProvider;
			_stringFormatter = stringFormatter;
		}

		protected IWordProvider WordProvider { get; }// => _wordProvider;

		public abstract IEnumerable<TagWithWord> CreateTagsWithWords(IEnumerable<Tag> tags);

		protected TagWithWord CreateTagWithWord(Tag tag, IWordProvider wordProvider)
		{
			return
				new TagWithWord(
					formatString: (wordToFormat, tagString) => _stringFormatter.FormatString(wordToFormat, tagString),
					tag: tag,
					word:
						tag.HasIdentifer
							? CreateWordFromIdentifiedTag(tag)
							: wordProvider.GetNewWord(tag));
		}

		private Word CreateWordFromIdentifiedTag(Tag tag)
		{
			TagWithWord tagWithWord =
				_tagsWithWordsToReuse.SingleOrDefault(
					tww => tww.Tag.Identifier == tag.Identifier);

			Word word;

			if (tagWithWord != null)
			{
				word = tagWithWord.Word;
			}
			else
			{
				word = WordProvider.GetNewWord(tag);

				_tagsWithWordsToReuse.Add(
					new TagWithWord(
						formatString: (wordToFormat, tagString) => _stringFormatter.FormatString(wordToFormat, tagString),
						tag: tag,
						word: word));
			}

			return word;
		}
	}
}
