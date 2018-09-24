using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TechJargonBot.Business.Data.Tags;

namespace TechJargonBot.Business
{
	public class Generator
    {
		private readonly ISentenceProvider _sentenceProvider;
		private readonly IWordSelector _wordSelector;
		private readonly IStringFormatter _stringFormatter;
		private readonly TagExtractor _tagExtractor;

		internal Generator(
			ISentenceProvider sentenceProvider,
			IWordSelector wordSelector,
			IStringFormatter stringFormatter)
		{
			_sentenceProvider = sentenceProvider;
			_wordSelector = wordSelector;
			_stringFormatter = stringFormatter;

			_tagExtractor =
				new TagExtractor(
					tagFactory: new TagFactory());
		}

		public String Generate()
		{
			return Generate(_sentenceProvider.GetSentence());
		}

		public String Generate(String sentence)
		{
			IEnumerable<Tag> tags = _tagExtractor.ExtractTags(sentence);

			List<TagWithWord> tagsWithRandomWords =
				_wordSelector.CreateTagsWithWords(tags).ToList();

			return
				RemoveHashesFromInsideHashtags(
					ReplaceTagsWithRandomWords(
						sentence,
						tagsWithRandomWords));
		}

		private String ReplaceTagsWithRandomWords(
			String sentence,
			IEnumerable<TagWithWord> tagsWithRandomWords)
		{
			foreach (var tagWithWord in tagsWithRandomWords)
				sentence = tagWithWord.ReplaceWordInSentence(sentence);

			return sentence;
		}

		private String RemoveHashesFromInsideHashtags(String sentence)
		{
			Byte numberOfHashesRemoved = 0;

			foreach (Match match in Regex.Matches(sentence, Regex.Escape("#")))
			{
				if (HashIsBetweenTwoLetters(sentence, match.Index, numberOfHashesRemoved))
				{
					sentence = sentence.Remove(match.Index - numberOfHashesRemoved, 1);
					numberOfHashesRemoved++;
				}
			}

			return sentence;
		}

		private Boolean HashIsBetweenTwoLetters(String sentence, Int32 index, Byte numberOfHashesRemoved)
		{
			if (index == 0)
				return false;

			Int32 positionOfLetterBeforeHash = (index - 1) - numberOfHashesRemoved;
			Int32 positionOfLetterAfterHash = (index + 1) - numberOfHashesRemoved;

			return
				Char.IsLetter(sentence[positionOfLetterBeforeHash])
				&&
				Char.IsLetter(sentence[positionOfLetterAfterHash]);
		}
	}
}
