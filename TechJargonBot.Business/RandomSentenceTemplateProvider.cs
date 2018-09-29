using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TechJargonBot.Business
{
	internal class RandomSentenceTemplateProvider : ISentenceTemplateProvider
	{
		private readonly List<String> _sentenceTemplates = new List<String>();

		public RandomSentenceTemplateProvider(Data.SentenceTemplateType sentenceTemplateType)
		{
			using (var reader = new StreamReader($@"..\..\..\TechJargonBot.Business\Data\{sentenceTemplateType.CsvFilename}.csv"))
			{
				while (!reader.EndOfStream)
					_sentenceTemplates.Add(
						Regex.Unescape(reader.ReadLine()));
			}
		}

		public String GetSentenceTemplate() => _sentenceTemplates.PickAtRandom();
	}
}
