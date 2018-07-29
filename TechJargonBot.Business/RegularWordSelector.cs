using System;
using System.Collections.Generic;
using System.Linq;
using TechJargonBot.Business.Data;
using TechJargonBot.Business.Data.Tags;

namespace TechJargonBot.Business
{
	internal class RegularWordSelector : IWordSelector
	{
		private readonly IStringFormatter _stringFormatter;

		public RegularWordSelector(
			IWordProvider wordProvider,
			IStringFormatter stringFormatter)
		{
			_wordProvider = wordProvider;
			_stringFormatter = stringFormatter;
		}

		private List<TagWithWord> _tagsWithWordsToReuse = new List<TagWithWord>();
		private readonly IWordProvider _wordProvider;

		public IEnumerable<TagWithWord> CreateTagsWithWords(IEnumerable<Tag> tags)
		{
			return
				tags.Select(tag =>
					new TagWithWord(
						stringFormatter: _stringFormatter,
						tag: tag,
						word:
							tag.HasIdentifer
								? CreateWordFromIdentifiedTag(tag)
								: _wordProvider.GetNewWord(tag)));
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
				word = _wordProvider.GetNewWord(tag);

				_tagsWithWordsToReuse.Add(
					new TagWithWord(
						_stringFormatter,
						tag,
						word));
			}

			return word;
		}
	}
}
