using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TechJargonBot.Business
{
	internal class RandomSentenceProvider : ISentenceProvider
	{
		private readonly List<String> _sentences = new List<String>();

		public RandomSentenceProvider(Data.SentenceType sentenceType)
		{
			using (var reader = new StreamReader($@"..\..\..\TechJargonBot.Business\Data\{sentenceType.CsvFilename}.csv"))
			{
				while (!reader.EndOfStream)
					_sentences.Add(
						Regex.Unescape(reader.ReadLine()));
			}
		}

		public String GetSentence() => _sentences.PickAtRandom();
	}
}
