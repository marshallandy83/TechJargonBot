using System;
using System.Linq;
using Moq;
using TechJargonBot.Business.Data.Tags;
using Xunit;

namespace TechJargonBot.Business
{
	public partial class RegularWordSelectorTests : IClassFixture<WordSelectorFixture>
	{
		private readonly WordSelectorFixture _wordSelectorFixture;

		public RegularWordSelectorTests(WordSelectorFixture wordSelectorFixture)
		{
			_wordSelectorFixture = wordSelectorFixture;
		}

		private void AssertResults(Tag[] tags, String[] expectedWords)
		{
			var selector =
				new RegularWordSelector(
					new MockWordProvider(),
					CreateMockStringFormatter());

			var tagsWithWords = selector.CreateTagsWithWords(tags).ToList();

			var words =
				tagsWithWords.Select(
					tagWithWord => tagWithWord.Word);

			var wordValues = words.Select(word => word.Forms.Single().Value);

			Assert.Equal(expected: expectedWords, actual: wordValues);
		}

		[Fact]
		public void TestGetWords_GetsNewWordFromWordProvider_WhenCalledWithSingleTag()
		{
			AssertResults(
				tags: new [] { new Tag(CreateTagString("[Tag]"), (_) => true, _wordSelectorFixture.MockTagReplacer, false) },
				expectedWords: new [] { "Word 1" });
		}

		[Fact]
		public void TestGetWords_GetsNewWordFromWordProvider_WhenCalledWithSingleIdentifiedTag()
		{
			AssertResults(
				tags: new[] { new Tag(CreateTagString("[Tag]"), 1, (_) => true, _wordSelectorFixture.MockTagReplacer, false) },
				expectedWords: new[] { "Word 1" });
		}

		[Fact]
		public void TestGetWords_GetsNewWordsFromWordProvider_WhenCalledWithMultipleTags()
		{
			AssertResults(
				tags: new[]
				{
					new Tag(CreateTagString("[Tag]"), (_) => true, _wordSelectorFixture.MockTagReplacer, false),
					new Tag(CreateTagString("[Tag]"), (_) => true, _wordSelectorFixture.MockTagReplacer, false)
				},
				expectedWords: new[] { "Word 1", "Word 2" });
		}

		[Fact]
		public void TestGetWords_ReusesWord_WhenSameIdentifierUsedMultipleTimes()
		{
			AssertResults(
				tags:
					new[]
					{
						new Tag(CreateTagString("[Tag]"), 1, (_) => true, _wordSelectorFixture.MockTagReplacer, false),
						new Tag(CreateTagString("[Tag]"), 1, (_) => true, _wordSelectorFixture.MockTagReplacer, false)
					},
				expectedWords: new[] { "Word 1", "Word 1" });
		}

		[Fact]
		public void TestGetWords_ReusesWord_WhenMultipleIdentifiersUsedMultipleTimes()
		{
			AssertResults(
				tags:
					new[]
						{
							new Tag(CreateTagString("[Tag]"), 1, (_) => true, _wordSelectorFixture.MockTagReplacer, false),
							new Tag(CreateTagString("[Tag]"), 1, (_) => true, _wordSelectorFixture.MockTagReplacer, false),
							new Tag(CreateTagString("[Tag]"), 2, (_) => true, _wordSelectorFixture.MockTagReplacer, false),
							new Tag(CreateTagString("[Tag]"), 2, (_) => true, _wordSelectorFixture.MockTagReplacer, false)
						},
				expectedWords:
					new[]
						{
							"Word 1",
							"Word 1",
							"Word 2",
							"Word 2"
					});
		}

		[Fact]
		public void TestGetWords_ReusesWord_WhenIdentifiedTagsAndNonIdentifiedTagsUsed()
		{
			AssertResults(
				tags:
					new[]
						{
							new Tag(CreateTagString("[Tag]"), 1, (_) => true, _wordSelectorFixture.MockTagReplacer, false),
							new Tag(CreateTagString("[Tag]"), 1, (_) => true, _wordSelectorFixture.MockTagReplacer, false),
							new Tag(CreateTagString("[Tag]"), (_) => true, _wordSelectorFixture.MockTagReplacer, false)
						},
				expectedWords:
					new[]
						{
							"Word 1",
							"Word 1",
							"Word 2"
					});
		}

		private IStringFormatter CreateMockStringFormatter()
		{
			var mockStringFormatter = new Mock<IStringFormatter>();

			return mockStringFormatter.Object;
		}

		private TagString CreateTagString(String rawTagString)
		{
			return new TagString(rawTagString, rawTagString);
		}
	}
}
