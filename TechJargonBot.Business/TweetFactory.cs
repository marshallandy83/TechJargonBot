using System;

namespace TechJargonBot.Business
{
	internal abstract partial class TweetFactory
	{
		public abstract String CreateTweet(Generator sentenceGenerator);
	}
}
