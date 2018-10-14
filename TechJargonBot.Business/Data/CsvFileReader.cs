using System;
using System.Collections.Generic;
using System.IO;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Business.Data
{
	internal class CsvFileReader : IDataReader
	{
		public IEnumerable<Word> ReadWords(
			String wordTypeName,
			Func<String[], Word> createWord)
		{
			using (var reader = new StreamReader($@"..\..\..\TechJargonBot.Business\Data\{wordTypeName}List.csv"))
			{
				SkipHeaders(reader);

				while (!reader.EndOfStream)
				{
					String line = reader.ReadLine();
					String[] values = line.Split(',');

					yield return createWord(values);
				}
			}
		}

		private static void SkipHeaders(StreamReader reader) => reader.ReadLine();
	}
}
