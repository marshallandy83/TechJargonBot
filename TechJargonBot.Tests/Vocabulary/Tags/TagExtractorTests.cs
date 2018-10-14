using System;
using System.Linq;
using Xunit;

namespace TechJargonBot.Vocabulary.Tags
{
	public class TagExtractorTests
	{
		[Theory]
		[InlineData(new String[0], "")]
		[InlineData(new String[0], "do this")]
		[InlineData(new String[] { "[do]"}, "[do] this")]
		[InlineData(new String[] { "[do]", "[a thing]" }, "[do] [a thing]")]
		[InlineData(new String[] { "[do]", "[thing]" }, "[do] this [thing]")]
		[InlineData(new String[] { "[do]", "[thing]" }, "Also, [do] this [thing]")]
		[InlineData(new String[] { "[do]", "[thing]" }, "[do] this [thing] too")]
		[InlineData(new String[] { "[do]", "[thing]" }, "Also, [do] this [thing] too")]
		[InlineData(new String[] { "[do]", "[thing]", "[thing]" }, "[do] this [thing] and this [thing]")]
		[InlineData(new String[] { "[do]", "[more complicated thing]" }, "[do] this [more complicated thing]")]
		[InlineData(new String[] { "[do]", "[indentifiable]", "[things]" }, "[do1] [indentifiable2] [things3]")]
		public void TestExtractTags(
			String[] expectedTagStrings,
			String sentence)
		{
			// TagFactory needs to be mocked
			var result = new TagExtractor(new TagFactory()).ExtractTags(sentence).ToArray();

			Assert.Equal(expectedTagStrings, result.Select(tag => tag.TagString.Sanitised));
		}
	}
}
