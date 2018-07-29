using System;
using Xunit;

namespace TechJargonBot.Business.Data
{
	public class HashtagSuitabilityProviderTests
	{
		[Theory]
		[InlineData(new String[] { "suitableWord" }, true)]
		[InlineData(new String[] { "suitable word" }, true)]
		[InlineData(new String[] { "1unsuitableWord" }, false)]
		[InlineData(new String[] { "suitableWord1" }, true)]
		[InlineData(new String[] { "unsuitable&Word" }, false)]
		[InlineData(new String[] { "suitableWord", "1unsuitableWord" }, false)]
		public void TestIsSuitableForHashtag(String[] words, Boolean expectedSuitability)
		{
			Assert.Equal(expectedSuitability, HashtagSuitabilityProvider.IsSuitableForHashtag(words));
		}
	}
}
