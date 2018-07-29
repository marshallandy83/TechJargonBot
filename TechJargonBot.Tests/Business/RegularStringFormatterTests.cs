using System;
using Moq;
using TechJargonBot.Business.Data;
using TechJargonBot.Business.Data.Tags;
using Xunit;

namespace TechJargonBot.Business
{
	public class RegularStringFormatterTests
	{
		[Theory]
		[InlineData("patched", "patched", "[done]")]
		[InlineData("Patched", "patched", "[Done]")]
		[InlineData("PATCHED", "patched", "[DONE]")]
		public void TestFormatString(
			String expectedString,
			String inputString,
			String tag)
		{
			Assert.Equal(
				expected: expectedString,
				actual: new RegularStringFormatter().FormatString(inputString, new TagString(tag, tag)));
		}
	}
}
