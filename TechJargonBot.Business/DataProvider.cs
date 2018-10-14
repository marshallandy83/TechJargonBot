using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TechJargonBot.Business.Data;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Business
{
	internal class DataProvider : IDataProvider
	{
		private readonly IDataReader _dataReader;
		private readonly Lazy<ReadOnlyCollection<List<Word>>> _getWordLists;

		public DataProvider(IDataReader dataReader)
		{
			_dataReader = dataReader;

			_getWordLists =
				new Lazy<ReadOnlyCollection<List<Word>>>(() => ReadWordLists());
		}

		public ReadOnlyCollection<List<Word>> GetWordLists()
		{
			return _getWordLists.Value;
		}

		private ReadOnlyCollection<List<Word>> ReadWordLists()
		{
			return
				new ReadOnlyCollection<List<Word>>(
					new List<List<Word>>
					{
						ReadWords("Verb", (values) => new Verb(values)),
						ReadWords("Adjective", (values) => new Adjective(values)),
						ReadWords("Noun", (values) => new Noun(values)),
						ReadWords("Person", (values) => new Person(values))
					});
		}

		private List<Word> ReadWords(
			String wordTypeName,
			Func<String[], Word> createWord)
		{
			return _dataReader.ReadWords(wordTypeName, createWord).ToList();
		}
	}
}
