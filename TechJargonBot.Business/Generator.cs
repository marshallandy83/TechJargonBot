using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TechJargonBot.Business.WordSelection;
using TechJargonBot.Vocabulary;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Business
{
	public class Generator
    {
		private readonly ISentenceTemplateProvider _sentenceTemplateProvider;
		private readonly WordSelector _wordSelector;
		private readonly IStringFormatter _stringFormatter;
		private readonly TagExtractor _tagExtractor;

		internal Generator(
			ISentenceTemplateProvider sentenceProvider,
			WordSelector wordSelector,
			IStringFormatter stringFormatter)
		{
			_sentenceTemplateProvider = sentenceProvider;
			_wordSelector = wordSelector;
			_stringFormatter = stringFormatter;

			_tagExtractor =
				new TagExtractor(
					tagFactory: new TagFactory());
		}

		internal Sentence Generate()
		{
			return Generate(_sentenceTemplateProvider.GetSentenceTemplate());
		}

		internal Sentence Generate(String sentenceTemplate)
		{
			IEnumerable<Tag> tags = _tagExtractor.ExtractTags(sentenceTemplate);

			List<TagWithWord> tagsWithRandomWords =
				_wordSelector.CreateTagsWithWords(tags).ToList();

			return
				new Sentence(
					tagsWithWords:
						tagsWithRandomWords,
					text:
						RemoveHashesFromInsideHashtags(
							ReplaceTagsWithRandomWords(
								sentenceTemplate,
								tagsWithRandomWords)));
		}

		private String ReplaceTagsWithRandomWords(
			String sentenceTemplate,
			IEnumerable<TagWithWord> tagsWithRandomWords)
		{
			foreach (var tagWithWord in tagsWithRandomWords)
				sentenceTemplate = tagWithWord.ReplaceWordInSentence(sentenceTemplate);

			return sentenceTemplate;
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
