using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace TechJargonBot.Business.Data.Tags
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

		[Fact]
		public void TestCreateTag_ReturnsCorrectTag_WithSentenceContainingTagWithIdentifier()
		{
			var tagString = "[do1]";

			var tag = new TagFactory().CreateTag(tagString);

			Assert.Equal("[do]", tag.TagString.Sanitised);
			Assert.Equal(1, tag.Identifier);
		}

		[Fact]
		public void TestCreateTag_ReturnsCorrectTag_WithSentenceContainingTagWithHashtagMarker()
		{
			var tagString = "[#thing]";

			var tag = new TagFactory().CreateTag(tagString);

			Assert.Equal("[thing]", tag.TagString.Sanitised);
		}

		[Fact]
		public void TestCreateTag_ReturnsCorrectTag_WithSentenceContainingTagWithIdentifierAndHashtagMarker()
		{
			var tagString = "[#thing1]";

			var tag = new TagFactory().CreateTag(tagString);

			Assert.Equal("[thing]", tag.TagString.Sanitised);
		}
	}
}
