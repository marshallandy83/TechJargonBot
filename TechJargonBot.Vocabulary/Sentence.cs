using System;
using System.Collections.Generic;
using System.Linq;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Vocabulary
{
	public class Sentence
	{
		public Sentence(List<TagWithWord> tagsWithWords, String text)
		{
			TagsWithWords = tagsWithWords;
			Text = text;
		}

		public List<TagWithWord> TagsWithWords { get; }
		public String Text { get; }
		public IEnumerable<Word> AllWords => TagsWithWords.Select(tagWithWord => tagWithWord.Word);
	}
}
