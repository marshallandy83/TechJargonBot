using System;
using System.Collections.Generic;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Business.Data
{
	internal interface IDataReader
	{
		IEnumerable<Word> ReadWords(
			String wordTypeName,
			Func<String[], Word> createWord);
	}
}