using System;
using System.Collections.Generic;
using Moq;
using TechJargonBot.Business.WordSelection;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Business
{
	public class GeneratorFixture
	{
		internal String GenerateSentence(
			String sentence,
			IEnumerable<TagWithWord> tagsWithWords)
		{
			return
				new Generator(
					sentenceProvider: null,
					wordSelectorFactory: CreateMockWordSelectorFactory(tagsWithWords),
					stringFormatter: new RegularStringFormatter())
				.Generate(sentence)
				.Text;
		}

		private IWordSelectorFactory CreateMockWordSelectorFactory(IEnumerable<TagWithWord> tagsWithWords)
		{
			var mockWordSelectorFactory = new Mock<IWordSelectorFactory>();
			mockWordSelectorFactory.Setup(wordSelectorFactory => wordSelectorFactory.Create()).Returns(CreateMockWordProvider(tagsWithWords));
			return mockWordSelectorFactory.Object;
		}

		private WordSelector CreateMockWordProvider(IEnumerable<TagWithWord> tagsWithWords)
		{
			var mockWordProvider = new Mock<WordSelector>();

			mockWordProvider.Setup(
				wordProvider => wordProvider.CreateTagsWithWords(
					It.IsAny<IEnumerable<Tag>>()))
					.Returns(tagsWithWords);

			return mockWordProvider.Object;
		}
	}
}