using System;
using System.Collections.Generic;

namespace TechJargonBot.Business.Data
{
	internal interface IDataReader
	{
		IEnumerable<Word> ReadWords(
			String wordTypeName,
			Func<String[], Word> createWord);
	}
}