using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Business
{
	internal class RandomSentenceTemplateProvider : ISentenceTemplateProvider
	{
		private readonly List<String> _sentenceTemplates = new List<String>();

		public RandomSentenceTemplateProvider(SentenceTemplateType sentenceTemplateType)
		{
			using (var reader = new StreamReader($@"..\..\..\TechJargonBot.Business\Data\{sentenceTemplateType.CsvLocation}\List.csv"))
			{
				while (!reader.EndOfStream)
					_sentenceTemplates.Add(
						Regex.Unescape(reader.ReadLine()));
			}
		}

		public String GetSentenceTemplate() => _sentenceTemplates.PickAtRandom();
	}
}
