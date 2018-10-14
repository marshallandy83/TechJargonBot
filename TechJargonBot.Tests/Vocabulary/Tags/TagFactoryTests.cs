using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace TechJargonBot.Vocabulary.Tags
{
	public class TagFactoryTests
	{
		[Fact]
		public void TestCreateTag_ReturnsCorrectSuitabilityPredicate()
		{
			var factory = new TagFactory();
			var word = factory.CreateTag("[#doing]");

			var words =
				new Word[]
					{
						new MockWord(isSuitableForHashtag: false),
						new MockWord(isSuitableForHashtag: true)
					};

			var includedWords =
				words.Where(word.IsSuitable);

			Assert.True(includedWords.All(w => w.IsSuitableForHashtag));
		}

		private class MockWord : Word
		{
			public MockWord(Boolean isSuitableForHashtag)
			{
				IsSuitableForHashtag = isSuitableForHashtag;
			}
		}

		[Theory]
		[InlineData("[thing]", "[thing]", 0, false, false)]
		[InlineData("[thing with identifier1]", "[thing with identifier]", 1, false, false)]
		[InlineData("[#thing for hashtag]", "[thing for hashtag]", 0, true, false)]
		[InlineData("[mandatory thing*]", "[mandatory thing]", 0, false, true)]
		[InlineData("[#thing with identifier for hashtag1]", "[thing with identifier for hashtag]", 1, true, false)]
		[InlineData("[mandatory thing with identifier1*]", "[mandatory thing with identifier]", 1, false, true)]
		[InlineData("[#mandatory thing for hashtag*]", "[mandatory thing for hashtag]", 0, true, true)]
		[InlineData("[#mandatory thing with identifier for hashtag1*]", "[mandatory thing with identifier for hashtag]", 1, true, true)]
		public void TestCreateTag_ReturnsCorrectTag_WithSentenceContainingTagWithIdentifier(
			String tagString,
			String expectedSanitisedTagString,
			Int32 expectedIdentifier,
			Boolean expectedIsForHashtag,
			Boolean expectedIsMandatory)
		{
			var tag = new TagFactory().CreateTag(tagString);

			Assert.Equal(expectedSanitisedTagString, tag.TagString.Sanitised);
			Assert.Equal(expectedIdentifier, tag.Identifier);
			Assert.Equal(expectedIsForHashtag, tag.IsForHashtag);
			Assert.Equal(expectedIsMandatory, tag.IsForMandatoryWord);
		}
	}
}
