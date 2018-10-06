using System;
using System.Collections.Generic;
using TechJargonBot.Business.Data.Tags;

namespace TechJargonBot.Business.Data
{
	internal class Sentence
	{
		public Sentence(List<TagWithWord> tagsWithWords, String text)
		{
			TagsWithWords = tagsWithWords;
			Text = text;
		}

		public List<TagWithWord> TagsWithWords { get; }
		public String Text { get; }
	}
}
