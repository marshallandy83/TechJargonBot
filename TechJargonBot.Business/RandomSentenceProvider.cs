using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TechJargonBot.Business
{
	internal class RandomSentenceProvider : ISentenceProvider
	{
		private readonly Random _randomNumberGenerator;
		private readonly List<String> _sentences = new List<String>();

		public RandomSentenceProvider(Random randomNumberGenerator)
		{
			_randomNumberGenerator = randomNumberGenerator;

			using (var reader = new StreamReader($@"..\..\..\TechJargonBot.Business\Data\Sentences.csv"))
			{
				while (!reader.EndOfStream)
					_sentences.Add(
						Regex.Unescape(reader.ReadLine()));
			}
		}

		public String GetSentence()
		{
			return
				_sentences
					.PickAtRandom(
						_randomNumberGenerator);
		}
	}
}
